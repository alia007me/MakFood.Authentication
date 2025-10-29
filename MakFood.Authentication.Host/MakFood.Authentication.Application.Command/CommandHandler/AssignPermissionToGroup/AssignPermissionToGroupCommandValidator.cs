using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.Authentication.Application.Command.CommandHandler.AssignPermissionToGroup
{
    public class AssignPermissionToGroupCommandValidator : AbstractValidator<AssignPermissionToGroupCommand>
    {
        public AssignPermissionToGroupCommandValidator()
        {
            RuleFor(x => x.Service).NotEmpty().WithMessage("Service Name is necessary");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name of api is necessary");
            RuleFor(x => x.GroupName).NotEmpty().WithMessage("GroupName is necessary");
        }
    }
}
