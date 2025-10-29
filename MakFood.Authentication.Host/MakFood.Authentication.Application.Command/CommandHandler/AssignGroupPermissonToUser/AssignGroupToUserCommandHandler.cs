using MakFood.Authentication.Domain.Model.Contracts;
using MakFood.Authentication.Domain.Model.Entities;
using MakFood.Authentication.Infraustraucture.Contract;
using MakFood.Authentication.Infraustraucture.Substructure.Base.ApplicationException;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.Authentication.Application.Command.CommandHandler.AssignGroupPermissonToUser
{
    public class AssignGroupToUserCommandHandler : IRequestHandler<AssignGroupToUserCommand, AssignGroupToUserCommandResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AssignGroupToUserCommandHandler(IUserRepository userRepository, IGroupRepository groupRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _groupRepository = groupRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<AssignGroupToUserCommandResponse> Handle(AssignGroupToUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserAsync(request.Username , cancellationToken);
            if (user == null)
                throw new ObjectNotFoundApplicationException("User Not Found !");
            
            var group = await _groupRepository.GetGroupAsync(request.GroupName , cancellationToken);
            if (group == null)
                throw new ObjectNotFoundApplicationException("Group Not Found !");

            var existingUserGroup = await _userRepository.GetUserGroupAsync(user.Id,group.Id,cancellationToken);
            if (existingUserGroup != null)
                throw new ObjectExistingInDatabaseApplicationException("This User Has This Group");

            var userGroup = CreateUserGroup(user.Id,group.Id);
            user.AddGroupsToUser(userGroup);
            var savingResult = await _unitOfWork.Commit(cancellationToken);
            if (savingResult == 0)
                throw new OperationFailedApplicationException("Nothing Added To The DataBase");

            var response = new AssignGroupToUserCommandResponse() { Success = true };
            return response;


        }
        #region Private Methods
        private UserGroup CreateUserGroup(Guid userId , uint groupId)
        {
            return new UserGroup(groupId, userId);
        }
        #endregion
    }
}
