using MakFood.Authentication.Domain.Model.Entities;
using MakFood.Authentication.Domain.Model.Enums;
using MakFood.Authentication.Infraustraucture.Context;
using MakFood.Authentication.Infraustraucture.Substructure.Helpers;
using System;
using System.Linq;

namespace MakFood.Authentication.Infraustraucture.Repositories.EF.Repository
{
    public static class DataInitializer
    {
        public static void SeedSuperAdmin(this AuthDbContext context)
        {

            if (context.Users.Any(u => u.Role == Role.SuperAdmin))
                return;

            const string defaultUsername = "Aunt";
            const string defaultPassword = "StrongPass123!";
            const string defaultPhone = "09196252346";

            var passwordHash = HashHelper.ComputeSha256(defaultPassword);
            var superAdmin = new User(
                username: defaultUsername,
                password: passwordHash,  
                gmail: "aunt@gmail.com", 
                phonenumber: defaultPhone,
                role: Role.SuperAdmin
            );


            context.Users.Add(superAdmin);
            context.SaveChanges();
        }
    }
}