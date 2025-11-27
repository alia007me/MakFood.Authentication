using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.Authentication.Application.Command.CommandHandler.DeclaringPermission
{
    public class DeclaringPermissionCommandValidator : AbstractValidator<DeclaringPermissionCommand>
    {
        public DeclaringPermissionCommandValidator()
        {
            RuleFor(x => x.Service).NotEmpty().WithMessage("Service Name Can't Be Empty");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Method Name Can't Be Empty");

        }   
    }
}
