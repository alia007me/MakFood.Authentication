using MakFood.Authentication.Infraustraucture.Substructure.Base.ApplicationException;

namespace MakFood.Authentication.Application.Command.Exception
{
    public class NoPermissionChangedException : NoChangesApplicationException
    {
        public NoPermissionChangedException() :base("No permission effected") { }
    }


}
