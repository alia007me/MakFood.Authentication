using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.Authentication.Application.Command.Command.Handler.DeclaringPermission
{
    public class DeclaringPermissionCommandResponse
    {
        public static DeclaringPermissionCommandResponse Succeed => new()
        {
            Success = true,
        };
        public bool Success { get; set; }
    }
}
