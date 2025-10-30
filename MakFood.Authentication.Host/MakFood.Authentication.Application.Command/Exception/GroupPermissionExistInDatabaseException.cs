using MakFood.Authentication.Infraustraucture.Substructure.Base.ApplicationException;

namespace MakFood.Authentication.Application.Command.CommandHandler.AssignPermissionToGroup
{

    public class GroupPermissionExistInDatabaseException : ObjectExistingInDatabaseApplicationException
    {
        public GroupPermissionExistInDatabaseException() : base("GroupPermission Exist in Database") { }
    }

}
