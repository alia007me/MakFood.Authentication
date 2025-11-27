using MakFood.Authentication.Infraustraucture.Substructure.Base.ApplicationException;

namespace MakFood.Authentication.Application.Command.Exception
{

    public class NoGroupChangedException : NoChangesApplicationException
    {
        public NoGroupChangedException() : base("No Group Added") { }
    }


}
