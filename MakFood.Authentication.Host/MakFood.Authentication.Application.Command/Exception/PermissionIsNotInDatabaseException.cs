using MakFood.Authentication.Infraustraucture.Substructure.Base.ApplicationException;

namespace MakFood.Authentication.Application.Command.CommandHandler.AssignPermissionToGroup
{
    public class PermissionIsNotInDatabaseException : ObjectNotFoundApplicationException
    {
        public PermissionIsNotInDatabaseException() : base("Permission Exist in Database") { }
    }

}
