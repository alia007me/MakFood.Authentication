using MakFood.Authentication.Infraustraucture.Substructure.Base.ApplicationException;

namespace MakFood.Authentication.Application.Command.Exception
{

    public class GroupIsNotInDatabaseException : NotFoundApplicationException
    {
        public GroupIsNotInDatabaseException() : base("Group is not in database") { }
    }


}
