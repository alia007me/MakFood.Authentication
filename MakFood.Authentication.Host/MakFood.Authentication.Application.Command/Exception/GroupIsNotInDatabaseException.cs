using MakFood.Authentication.Infraustraucture.Substructure.Base.ApplicationException;

namespace MakFood.Authentication.Application.Command.Exception
{

    public class GroupIsNotInDatabaseException : ObjectNotFoundApplicationException
    {
        public GroupIsNotInDatabaseException() : base("Group is not in database") { }
    }


}
