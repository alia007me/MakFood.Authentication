using MakFood.Authentication.Domain.Model.Base;
using MakFood.Authentication.Domain.Model.Enums;
using MakFood.Authentication.Infraustraucture.Substructure.Base.DomainExceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MakFood.Authentication.Domain.Model.Entities
{
    public class User : BaseEntity<Guid>
    {
        private User() { }
        public User(string username, string password, string? gmail, string phonenumber, Role role)
        {
            CheckUsername(username);
            CheckPassword(password);
            CheckGmail(gmail);


            Id = Guid.NewGuid();
            Username = username;
            Password = password;
            Gmail = gmail;
            Phonenumber = phonenumber;
            Role = role;
        }

        public User(string username, string password, string phonenumber, Role role)
        {
            Username = username;
            Password = password;
            Phonenumber = phonenumber;
            this.role = role;
        }

        private readonly List<UserGroup> _groups = new List<UserGroup>();
        private Role role;

        public Guid Id { get; set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string? Gmail { get; private set; }
        public string Phonenumber { get; private set; }
        public Role Role { get; private set; } 
        public string PasswordHash { get; set; }
        public bool IsSuperAdmin { get; set; }
        public DateTime CreatedAt { get; set; }

        public IEnumerable<UserGroup> Groups => _groups.AsReadOnly();


        public void AddGroupsToUser(UserGroup userGroup)
        {
            if (_groups.Any(c => c == userGroup))
            {
                throw new ValidationFailedDomainException("This User Has This Group");
            }
                _groups.Add(userGroup);
        }



        #region Private Methods
        private void CheckUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ValidationFailedDomainException("Username Can't Be Null");
        }

        private void CheckPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password)) throw new ValidationFailedDomainException("Password Can't Be Null !");
        }

        private void CheckGmail(string gmail)
        {
            var pattern = @"^[a-zA-Z0-9._%+-]+@gmail\.com$";
            if (string.IsNullOrWhiteSpace(gmail) || !Regex.IsMatch(gmail, pattern))
            {
                throw new ValidationFailedDomainException("Only Gmail Is Allowed ");
            }
        }

        #endregion

    }
}
