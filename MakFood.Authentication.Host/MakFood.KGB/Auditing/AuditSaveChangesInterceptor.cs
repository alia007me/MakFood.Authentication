using MakFood.KGB.Auditing.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace MakFood.KGB.Auditing
{
    public class AuditSaveChangesInterceptor : SaveChangesInterceptor
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuditSaveChangesInterceptor(IServiceProvider serviceProvider, IHttpContextAccessor httpContextAccessor)
        {
            _serviceProvider = serviceProvider;
            _httpContextAccessor = httpContextAccessor;
        }
        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            if (eventData.Context == null) return result;

            var auditEntries = ProcessAuditEntries(eventData.Context);

            if (auditEntries.Count > 0)
            {
                using var scope = _serviceProvider.CreateScope();
                var auditDb = scope.ServiceProvider.GetRequiredService<AuditLoggingDbContext>();

                if (auditDb != null)
                {
                    foreach (var a in auditEntries)
                    {
                        var audit = a.ToAuditLog();
                        auditDb.AuditLogs.Add(audit);
                    }
                    await auditDb.SaveChangesAsync(cancellationToken);
                }
            }

            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        // در کلاس AuditSaveChangesInterceptor

        private List<AuditEntry> ProcessAuditEntries(DbContext context)
        {
            var userId = "";
            var userName = "";
            var correlation = Guid.NewGuid().ToString();
            var ipAddress = ""; // 🟢 اضافه شده
            var device = "";    // 🟢 اضافه شده

            if (_httpContextAccessor.HttpContext != null)
            {
                var http = _httpContextAccessor.HttpContext;
                correlation = http.TraceIdentifier ?? correlation;

                // 🟢 جمع‌آوری IPAddress از اتصال
                ipAddress = http.Connection.RemoteIpAddress?.ToString() ?? "";

                // 🟢 جمع‌آوری Device (از User-Agent Header)
                if (http.Request.Headers.TryGetValue("User-Agent", out var userAgentHeader))
                {
                    device = userAgentHeader.ToString();
                }

                if (http.User?.Identity?.IsAuthenticated == true)
                {
                    userId = http.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "";
                    userName = http.User.Identity.Name ?? "";
                }
            }

            var auditEntries = new List<AuditEntry>();

            foreach (var entry in context.ChangeTracker.Entries()
                .Where(e => e.State != EntityState.Detached && e.State != EntityState.Unchanged))
            {
                if (entry.Entity is AuditLog) continue;

                var auditEntry = new AuditEntry(entry)
                {
                    TableName = entry.Metadata.GetTableName() ?? entry.Metadata.Name,
                    Action = entry.State.ToString(),
                    UserId = userId,
                    UserName = userName,
                    CorrelationId = correlation,
                    IPAddress = ipAddress,
                    Device = device
                };

                var keyValues = new Dictionary<string, object?>();
                foreach (var prop in entry.Properties.Where(p => p.Metadata.IsPrimaryKey()))
                {
                    keyValues[prop.Metadata.Name] = prop.CurrentValue ?? prop.OriginalValue;
                }
                auditEntry.KeyValues = keyValues.Count > 0 ? JsonSerializer.Serialize(keyValues) : null;

                foreach (var prop in entry.Properties)
                {
                    if (prop.IsTemporary || prop.Metadata.IsPrimaryKey()) continue;

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.AfterValues[prop.Metadata.Name] = prop.CurrentValue;
                            break;
                        case EntityState.Deleted:
                            auditEntry.BeforeValues[prop.Metadata.Name] = prop.OriginalValue;
                            break;
                        case EntityState.Modified:
                            if (!Equals(prop.CurrentValue, prop.OriginalValue))
                            {
                                auditEntry.ChangedColumns.Add(prop.Metadata.Name);
                                auditEntry.BeforeValues[prop.Metadata.Name] = prop.OriginalValue;
                                auditEntry.AfterValues[prop.Metadata.Name] = prop.CurrentValue;
                            }
                            break;
                    }
                }

                if (auditEntry.BeforeValues.Count > 0 || auditEntry.AfterValues.Count > 0)
                {
                    auditEntries.Add(auditEntry);
                }
            }

            return auditEntries;
        }
    }
}