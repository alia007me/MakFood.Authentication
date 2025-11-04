using MakFood.Authentication.Infraustraucture.Substructure.Base.ApplicationException.BaseException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.Authentication.Infraustraucture.Substructure.Base.ApplicationException
{
    public class ObjectExistingInDatabaseApplicationException : BaseException.ApplicationException
    {
        public ObjectExistingInDatabaseApplicationException() { }
        public ObjectExistingInDatabaseApplicationException(string message) : base(message) { }
        public ObjectExistingInDatabaseApplicationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
