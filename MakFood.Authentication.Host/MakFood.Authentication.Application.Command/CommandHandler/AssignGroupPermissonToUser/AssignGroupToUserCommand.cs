using MakFood.Authentication.Application.Command.CommandHandler.Base;
using MakFood.Authentication.Application.Command.CommandHandler.Base.Extension;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.Authentication.Application.Command.CommandHandler.AssignGroupPermissonToUser
{
    public class AssignGroupToUserCommand : CommandBase, IRequest<AssignGroupToUserCommandResponse>
    {
        public string GroupName { get; set; }
        public string Username { get; set; }
        public override void Validate()
        {
            new AssignGroupToUserCommandValidator().Validate(this).ThrowIfNeeded();
        }
    }
}
