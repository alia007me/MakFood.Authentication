using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.Authentication.Domain.Model.Entities
{
    public class GroupPermission
    {

        public Group Group { get; private set; }
        public uint GroupId { get; private set; }
        public uint PermissionId { get; private set; }

    }
}
