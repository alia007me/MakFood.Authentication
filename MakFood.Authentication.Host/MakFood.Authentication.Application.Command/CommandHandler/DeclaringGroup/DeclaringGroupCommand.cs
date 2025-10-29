using MakFood.Authentication.Application.Command.CommandHandler.Base;
using MakFood.Authentication.Application.Command.CommandHandler.Base.Extension;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.Authentication.Application.Command.CommandHandler.DeclaringGroup
{
    public class DeclaringGroupCommand : CommandBase, IRequest<DeclaringGroupCommandResponse>
    {
        public string GroupName { get; set; }
        public string Description { get; set; }
        public override void Validate()
        {
            new DeclaringGroupCommandValidator().Validate(this).ThrowIfNeeded();

        }
    }
}
