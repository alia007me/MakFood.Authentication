using FluentValidation.Results;
using MakFood.Authentication.Infraustraucture.Substructure.Base.ApplicationException;
using MakFood.Authentication.Infraustraucture.Substructure.Base.DomainExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.Authentication.Application.Command.CommandHandler.Base.Extension
{
    public static class ValidateResultExtension
    {
        public static void ThrowIfNeeded(this ValidationResult validationResult)
        {
            var errors = validationResult.Errors;

            if (errors.Any())
                throw new InputValidationFailedApplicationException(string.Join(Environment.NewLine, errors.Select(e => e.ErrorMessage)));
        }

    }
}
