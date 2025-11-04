using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.Authentication.Infraustraucture.Substructure.Base.ApplicationException
{
    public class InputValidationFailedApplicationException : BaseException.ApplicationException
    {
        public InputValidationFailedApplicationException() { }

        public InputValidationFailedApplicationException(string message) : base(message) { }

        public InputValidationFailedApplicationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
