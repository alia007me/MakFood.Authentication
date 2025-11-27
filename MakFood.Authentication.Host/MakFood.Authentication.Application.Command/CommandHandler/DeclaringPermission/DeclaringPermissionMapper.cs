using MakFood.Authentication.Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.Authentication.Application.Command.CommandHandler.DeclaringPermission
{
    public static class DeclaringPermissionMapper
    {
        public static Permission ToModel(this DeclaringPermissionCommand command) 
            => new Permission(command.Service, command.Name, command.Description);
    }
}
