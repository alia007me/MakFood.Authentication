using MakFood.Authentication.Domain.Model.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MakFood.Authentication.Domain.Model.Entities
{
    public class User
    {
        private User() { }
        public User(string username, string password, string? gmail, string phonenumber, Role role)
        {
            CheckUsername(username);
            CheckPassword(password);
            CheckGmail(gmail);



            Username = username;
            Password = password;
            Gmail = gmail;
            Phonenumber = phonenumber;
            Role = role;
        }

        public Guid Id { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string? Gmail { get; private set; }
        public string Phonenumber { get; private set; }
        public DateTime LastLogin { get; private set; }
        public Role Role { get; private set; }


        public IList<UserGroup> Groups { get; private set; } = new List<UserGroup>();




        #region Private Methods
        private void CheckUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username Can't Be Null");
        }

        private void CheckPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Password Can't Be Null !");
        }

        private void CheckGmail(string gmail)
        {
            var pattern = @"^[a-zA-Z0-9._%+-]+@gmail\.com$";
            if (!string.IsNullOrWhiteSpace(gmail) || !Regex.IsMatch(gmail, pattern))
            {
                throw new ArgumentException("Only Gmail Is Allowed ");
            }
        }

        #endregion

    }
}
