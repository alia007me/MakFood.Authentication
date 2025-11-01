using MakFood.Authentication.Domain.Model.Entities;

namespace MakFood.Authentication.Application.Command.CommandHandler.DeclaringGroup
{
    public static class DeclaringGroupMapper
    {
        public static Group ToModel(this DeclaringGroupCommand command)
        {
            return new Group(command.Name, command.Description);
        }
    } 
}
