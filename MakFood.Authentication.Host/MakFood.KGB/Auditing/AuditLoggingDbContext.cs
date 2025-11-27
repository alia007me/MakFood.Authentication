using MakFood.KGB.Auditing.Entities;
using Microsoft.EntityFrameworkCore;

namespace MakFood.KGB.Auditing
{
    public class AuditLoggingDbContext : DbContext
    {
        public AuditLoggingDbContext(DbContextOptions<AuditLoggingDbContext> options) : base(options) { }
        public DbSet<AuditLog> AuditLogs { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuditLog>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.Action).HasMaxLength(50).IsRequired();
                b.Property(x => x.EntityName).HasMaxLength(200).IsRequired();
                b.Property(x => x.CorrelationId).HasMaxLength(200);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
