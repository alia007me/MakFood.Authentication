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

        public GroupPermission(uint permissionId)
        {
            PermissionId = permissionId;
        }

        public uint PermissionId { get; private set; }


    }
}
