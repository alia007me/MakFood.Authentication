using MakFood.Authentication.Domain.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.Authentication.Domain.Model.Entities
{
    public class UserGroup : BaseEntity<Guid>
    {
        public UserGroup(uint groupId)
        {
            GroupId = groupId;
        }

        public uint GroupId { get; private set; }
    }
}
