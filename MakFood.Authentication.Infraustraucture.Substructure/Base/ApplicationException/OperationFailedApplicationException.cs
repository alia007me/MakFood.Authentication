using MakFood.Authentication.Infraustraucture.Substructure.Base.ApplicationException.BaseException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.Authentication.Infraustraucture.Substructure.Base.ApplicationException
{
    public class OperationFailedApplicationException : BaseException.ApplicationException
    {
        public OperationFailedApplicationException() { }

        public OperationFailedApplicationException(string message) : base(message) { }

        public OperationFailedApplicationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
