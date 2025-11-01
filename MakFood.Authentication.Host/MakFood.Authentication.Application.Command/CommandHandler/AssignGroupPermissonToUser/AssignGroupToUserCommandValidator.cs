using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.Authentication.Application.Command.CommandHandler.AssignGroupPermissonToUser
{
    public class AssignGroupToUserCommandValidator : AbstractValidator<AssignGroupToUserCommand>
    {
        public AssignGroupToUserCommandValidator()
        {
            RuleFor(x=>x.Username).NotEmpty().WithMessage("Username Can't Be Null");
            RuleFor(x => x.GroupName).NotEmpty().WithMessage("GroupName Can't Be Null");
        }
    }
}
