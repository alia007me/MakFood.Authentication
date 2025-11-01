using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.Authentication.Infraustraucture.Substructure.Base.ApplicationException
{
    public class ObjectNotFoundApplicationException : BaseException.ApplicationException
    {
        public ObjectNotFoundApplicationException() { }
        public ObjectNotFoundApplicationException(string message) : base(message) { }
        public ObjectNotFoundApplicationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
