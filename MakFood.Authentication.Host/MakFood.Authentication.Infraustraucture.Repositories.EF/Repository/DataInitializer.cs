using MakFood.Authentication.Domain.Model.Entities;
using MakFood.Authentication.Domain.Model.Enums;
using MakFood.Authentication.Infraustraucture.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MakFood.Authentication.Infraustraucture.Substructure.Helpers;

namespace MakFood.Authentication.Infraustraucture.Repositories.EF.Repository
{
    public static class DataInitializer
    {
        public static void SeedSuperAdmin(this AuthDbContext context)
        {
            if (context.Users.Any(u => u.Role == Role.SuperAdmin)) return;
            var superAdmin = new User(
                username: "Aunt",
                password: "StrongPass123!",
                 gmail: null,
                phonenumber: "09196252346",
                role: Role.SuperAdmin);
            superAdmin.CreatedAt = DateTime.UtcNow;
            context.Users.Add(superAdmin);
            superAdmin.PasswordHash = HashHelper.ComputeSha256("StrongPass123!");
        }
 
    }
}

