using MakFood.Authentication.Application.Command.Exception;
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
using static MakFood.Authentication.Application.Command.CommandHandler.DeclaringGroup.DeclaringGroupCommandHandler;

namespace MakFood.Authentication.Application.Command.CommandHandler.AssignGroupToUser
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
            var user = await _userRepository.GetUserByIdAsync(request.UserId, cancellationToken);
            CheckUserExistance(user);

            var group = await _groupRepository.GetGroupByIdAsync(request.groupId, cancellationToken);
            CheckGroupExistance(group);

            await CheckUserGroupExistance(user.Id, group.Id, cancellationToken);


            var userGroup = CreateUserGroup(user.Id, group.Id);
            user.AddGroupsToUser(userGroup);
            var savingResult = await _unitOfWork.Commit(cancellationToken);
            savingResult.ThrowIfNoChanges<NoChangesApplicationException>();


            return AssignGroupToUserCommandResponse.Succeeded;


        }


        #region Private Methods
        private UserGroup CreateUserGroup(Guid userId, uint groupId)
        {
            return new UserGroup(groupId, userId);
        }
        private static void CheckGroupExistance(Group group)
        {
            if (group == null)
                throw new GroupIsNotInDatabaseException();
        }

        private static void CheckUserExistance(User user)
        {
            if (user == null)
                throw new UserIsNotInDatabaseException();
        }
        private async Task CheckUserGroupExistance(Guid userId, uint groupId, CancellationToken ct)
        {
            var result = await _userRepository.IsUserGroupExist(userId, groupId, ct);
            if (result)
                throw new UserGroupIsInDatabaseException();
        }
        #endregion
    }
}
