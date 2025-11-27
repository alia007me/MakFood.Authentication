using MakFood.KGB.Auditing.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Text.Json;

namespace MakFood.KGB.Auditing
{
    internal class AuditEntry
    {
        public AuditEntry(EntityEntry entry)
        {
            Entry = entry;
        }

        public EntityEntry Entry { get; }
        public string TableName { get; set; } = "";
        public string Action { get; set; } = "";
        public string? KeyValues { get; set; }
        public Dictionary<string, object?> BeforeValues { get; } = new();
        public Dictionary<string, object?> AfterValues { get; } = new();
        public List<string> ChangedColumns { get; } = new();
        public string UserId { get; set; } = "";
        public string UserName { get; set; } = "";
        public string CorrelationId { get; set; } = "";
        public string? MetadataJson { get; set; }
        public string IPAddress { get; internal set; }
        public string Device { get; internal set; }

        public AuditLog ToAuditLog()
        {
            return new AuditLog
            {
                UserId = UserId,
                UserName = UserName,
                Action = Action,
                EntityName = TableName,
                EntityId = KeyValues, 
                Before = BeforeValues.Count == 0 ? null : JsonSerializer.Serialize(BeforeValues),
                After = AfterValues.Count == 0 ? null : JsonSerializer.Serialize(AfterValues),
                TimestampUtc = DateTime.UtcNow,
                CorrelationId = CorrelationId,
                MetadataJson = MetadataJson,
            };
        }
    }
}
