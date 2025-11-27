using MakFood.Authentication.Infraustraucture.Substructure.Base.ApplicationException;

namespace MakFood.Authentication.Application.Command.Exception
{

    public class GroupPermissionExistInDatabaseException : ObjectExistingInDatabaseApplicationException
    {
        public GroupPermissionExistInDatabaseException() : base("GroupPermission Exist in Database") { }
    }

}
