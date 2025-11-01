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
        public string Service {  get; set; }
        public string Name { get; set; }
        public string GroupName { get; set; }
        public override void Validate()
        {
            new AssignPermissionToGroupCommandValidator().Validate(this).ThrowIfNeeded();
        }
    }
}
