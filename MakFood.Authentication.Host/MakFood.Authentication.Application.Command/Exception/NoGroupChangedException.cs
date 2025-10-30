using MakFood.Authentication.Infraustraucture.Substructure.Base.ApplicationException;

namespace MakFood.Authentication.Application.Command.CommandHandler.DeclaringGroup
{

    public class NoGroupChangedException : NoChangesApplicationException
    {
        public NoGroupChangedException() : base("No Group Added") { }
    }


}
