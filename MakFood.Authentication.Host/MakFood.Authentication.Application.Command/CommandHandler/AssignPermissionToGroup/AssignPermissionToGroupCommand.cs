using MakFood.Authentication.Application.Command.CommandHandler.Base;
using MakFood.Authentication.Application.Command.CommandHandler.Base.Extension;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.Authentication.Application.Command.CommandHandler.AssignPermissionToGroup
{
    public class AssignPermissionToGroupCommand : CommandBase, IRequest<AssignPermissionToGroupCommandResponse>
    {
        public uint permissionId { get; set; }
        public uint groupId { get; set; }

        public override void Validate()
        {
            new AssignPermissionToGroupCommandValidator().Validate(this).ThrowIfNeeded();
        }
    }
}
