using MakFood.Authentication.Infraustraucture.Substructure.Base.ApplicationException.BaseException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.Authentication.Infraustraucture.Substructure.Base.ApplicationException
{
    public class NoChangesApplicationException : BaseException.ApplicationException
    {
        public NoChangesApplicationException() { }

        public NoChangesApplicationException(string message) : base(message) { }

        public NoChangesApplicationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
