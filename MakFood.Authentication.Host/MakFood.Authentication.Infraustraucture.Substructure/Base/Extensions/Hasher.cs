using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.Authentication.Infraustraucture.Substructure.Base.Extensions
{
    public static class Hasher
    {
        private static string salt="ZX on security";
        public static string PasswordHasher(this string password)
        {
            password += salt;
            var passwordHash = Encoding.UTF8.GetBytes(password);
            var test = Convert.ToBase64String(MD5.HashData(passwordHash));
            return test;
            

        }
    }
}
