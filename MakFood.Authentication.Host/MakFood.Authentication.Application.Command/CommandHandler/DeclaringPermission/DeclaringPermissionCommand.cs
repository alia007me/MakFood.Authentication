using MakFood.Authentication.Application.Command.CommandHandler.Base;
using MakFood.Authentication.Application.Command.CommandHandler.Base.Extension;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.Authentication.Application.Command.Command.Handler.DeclaringPermission
{
    public class DeclaringPermissionCommand : CommandBase, IRequest<DeclaringPermissionCommandResponse>
    {
        public string Service {  get; set; }
        public string Method { get; set; }
        public string Description { get; set; }


        public override void Validate()
        {

            new DeclaringPermissionCommandValidator().Validate(this).ThrowIfNeeded();

        }
    }
}
