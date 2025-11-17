using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.Authentication.Domain.Model.Entities
{
    public class AuthUser
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Username { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;

        public string PasswordHash { get; set; } = default!;

        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
