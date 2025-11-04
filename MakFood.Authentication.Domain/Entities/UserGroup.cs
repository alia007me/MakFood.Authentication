using MakFood.Authentication.Domain.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.Authentication.Domain.Model.Entities
{
    public class UserGroup : BaseEntity<uint>
    {
        public UserGroup(uint groupId, Guid userId)
        {
            GroupId = groupId;
            UserId = userId;
        }
        public Guid UserId { get; private set; }

        public uint GroupId { get; private set; }
    }
}
