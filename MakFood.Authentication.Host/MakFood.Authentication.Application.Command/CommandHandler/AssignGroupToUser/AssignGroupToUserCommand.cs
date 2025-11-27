using MakFood.Authentication.Application.Command.CommandHandler.Base;
using MakFood.Authentication.Application.Command.CommandHandler.Base.Extension;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.Authentication.Application.Command.CommandHandler.AssignGroupToUser
{
    public class AssignGroupToUserCommand : CommandBase, IRequest<AssignGroupToUserCommandResponse>
    {
        public uint groupId { get; set; }
        public Guid UserId { get; set; }
        public override void Validate()
        {
            new AssignGroupToUserCommandValidator().Validate(this).ThrowIfNeeded();
        }
    }
}
