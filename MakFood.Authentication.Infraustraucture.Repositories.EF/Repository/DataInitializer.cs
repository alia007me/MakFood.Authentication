using MakFood.Authentication.Domain.Model.Entities;
using MakFood.Authentication.Domain.Model.Enums;
using MakFood.Authentication.Infraustraucture.Context;
using MakFood.Authentication.Infraustraucture.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.Authentication.Infraustraucture.Repositories.EF.Repository
{
    public static class DataInitializer
    {
        public static void SeedSuperAdmin(this AuthDbContext context)
        {
            if (context.Users.Any(u => u.IsSuperAdmin))
                return;
            var superAdmin = new User(
                username: "Aunt",
                password: "StrongPass123!", 
                phonenumber: "09196252346",
                role: Role.Admin
            );

            superAdmin.PasswordHash = HashPassword("Aunt");
            superAdmin.IsSuperAdmin = true;
            superAdmin.CreatedAt = DateTime.UtcNow;

            context.Users.Add(superAdmin);
        }
        private static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
