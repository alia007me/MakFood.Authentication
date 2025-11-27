using MakFood.Authentication.Infraustraucture.Substructure.Base.ApplicationException;

namespace MakFood.Authentication.Application.Command.Exception
{



    public class GroupIsInDatabaseException : NotFoundApplicationException
    {
        public GroupIsInDatabaseException() : base("This Group With This Name Is In Database") { }
    }

}
