using MakFood.Authentication.Infraustraucture.Substructure.Base.ApplicationException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.Authentication.Application.Command.Exception
{
    public class UserIsNotInDatabaseException : NotFoundApplicationException
    {
        public UserIsNotInDatabaseException() : base("User Is Not In Database") { }
    }
}
