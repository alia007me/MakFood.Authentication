using MakFood.Authentication.Domain.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.Authentication.Domain.Model.Entities
{
    public class GroupPermission : BaseEntity<uint>
    {

        public GroupPermission(uint permissionId, uint groupId)
        {
            PermissionId = permissionId;
            GroupId = groupId;
        }
        public uint GroupId { get; private set; }

        public uint PermissionId { get; private set; }


    }
}
