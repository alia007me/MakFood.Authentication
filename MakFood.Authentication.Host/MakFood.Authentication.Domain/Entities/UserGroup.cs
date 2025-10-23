using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.Authentication.Domain.Model.Entities
{
    public class UserGroup
    {
        public User User { get; private set; }
        public Guid UserId { get; private set; }

        public uint GroupId { get; private set; }
    }
}
