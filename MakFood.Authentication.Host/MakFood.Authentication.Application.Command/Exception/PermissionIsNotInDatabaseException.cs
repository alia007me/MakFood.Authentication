using MakFood.Authentication.Infraustraucture.Substructure.Base.ApplicationException;

namespace MakFood.Authentication.Application.Command.CommandHandler.AssignPermissionToGroup
{
    public class PermissionIsNotInDatabaseException : NotFoundApplicationException
    {
        public PermissionIsNotInDatabaseException() : base("Permission Exist in Database") { }
    }

}
