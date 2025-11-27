using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.Authentication.Application.Command.CommandHandler.AssignGroupToUser
{
    public class AssignGroupToUserCommandValidator : AbstractValidator<AssignGroupToUserCommand>
    {
        public AssignGroupToUserCommandValidator()
        {
            RuleFor(x=>x.UserId).NotEmpty().WithMessage("UserId Can't Be Null");
            RuleFor(x => x.groupId).NotEmpty().WithMessage("GroupId Can't Be Null");
        }
    }
}
