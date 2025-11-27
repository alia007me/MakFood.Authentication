using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.KGB.Auditing.Entities
{
    public class AuditLog
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string UserId { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string Action { get; set; } = default!;
        public string EntityName { get; set; } = default!;
        public string? EntityId { get; set; }
        public DateTime TimestampUtc { get; set; } = DateTime.UtcNow;
        public string CorrelationId { get; set; } = default!;

        public string? Before { get; set; }
        public string? After { get; set; }

        public string? MetadataJson { get; set; }

        public string? IPAddress { get; set; }
        public string? Device { get; set; }

        public AuditLog ToAuditLog()
        {
            return new AuditLog
            {
                IPAddress = IPAddress,
                Device = Device,
            };

        }
    }
}
