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
            RuleFor(x => x.permissionId).NotEmpty().WithMessage("permissionId is necessary");
            RuleFor(x => x.groupId).NotEmpty().WithMessage("groupId is necessary");
        }
    }
}
