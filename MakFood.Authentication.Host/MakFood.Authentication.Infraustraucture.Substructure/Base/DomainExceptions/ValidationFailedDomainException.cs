using MakFood.Authentication.Infraustraucture.Substructure.Base.DomainExceptions.BaseException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.Authentication.Infraustraucture.Substructure.Base.DomainExceptions
{
    public class ValidationFailedDomainException : DomainException
    {
        public ValidationFailedDomainException() { }

        public ValidationFailedDomainException(string message) : base(message)
        {

        }

        public ValidationFailedDomainException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
