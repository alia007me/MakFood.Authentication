using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.Authentication.Domain.Model.Entities.LoginAggregate
{
    public class LoginHistory
    {
        private LoginHistory() { }
        public LoginHistory(Guid userId, DateTime dateOfLoginOrLogout)
        {
            UserId = userId;
            DateOfLoginOrLogout = dateOfLoginOrLogout;
        }

        public Guid UserId { get; private set; }
        public DateTime DateOfLoginOrLogout { get; private set; }
    }
}
