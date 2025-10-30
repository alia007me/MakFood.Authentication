using MakFood.Authentication.Infraustraucture.Substructure.Base.ApplicationException;

namespace MakFood.Authentication.Application.Command.CommandHandler.DeclaringGroup
{



    public class GroupIsInDatabaseException : ObjectNotFoundApplicationException
    {
        public GroupIsInDatabaseException() : base("This Group With This Name Is In Database") { }
    }

}
