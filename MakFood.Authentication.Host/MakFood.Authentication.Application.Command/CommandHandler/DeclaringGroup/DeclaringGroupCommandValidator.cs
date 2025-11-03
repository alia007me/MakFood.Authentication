using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.Authentication.Application.Command.CommandHandler.DeclaringGroup
{
    public class DeclaringGroupCommandValidator : AbstractValidator<DeclaringGroupCommand>
    {
        public DeclaringGroupCommandValidator()
        {
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Group name can't be null !");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description Is neccessary in group !");
        }
    }
}
