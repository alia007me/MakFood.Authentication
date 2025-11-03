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

namespace MakFood.Authentication.Application.Command.CommandHandler.AssignPermissionToGroup
{
    public partial class AssignPermissionToGroupCommandHandler : IRequestHandler<AssignPermissionToGroupCommand, AssignPermissionToGroupCommandResponse>
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AssignPermissionToGroupCommandHandler(IPermissionRepository permissionRepository, IGroupRepository groupRepository, IUnitOfWork unitOfWork)
        {
            _permissionRepository = permissionRepository;
            _groupRepository = groupRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<AssignPermissionToGroupCommandResponse> Handle(AssignPermissionToGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await _groupRepository.GetGroupByIdAsync(request.groupId, cancellationToken);
            CheckGroupExistance(group);

            var permission = await _permissionRepository.GetPermissionByIdAsync(request.permissionId, cancellationToken);
            CheckPermissionExistance(permission);

            await CheckGroupPermissionExistance(group.Id, permission.Id, cancellationToken);


            var groupPermission = CreateGroupPermission(group.Id, permission.Id);
            group.AddPermissionsToGroup(groupPermission);

            var savingResult = await _unitOfWork.Commit(cancellationToken);

            savingResult.ThrowIfNoChanges<NoChangesApplicationException>();

            return AssignPermissionToGroupCommandResponse.Succeeded;




        }




        #region Private Methods
        private async Task CheckGroupPermissionExistance(uint groupId, uint permissionId, CancellationToken ct)
        {
            var existingGroup = await _groupRepository.IsGroupPermissionExist(groupId, permissionId, ct);

            if (existingGroup)
                throw new GroupPermissionExistInDatabaseException();
        }

        private static void CheckPermissionExistance(Permission permission)
        {
            if (permission is null)
                throw new PermissionIsNotInDatabaseException();
        }

        private static void CheckGroupExistance(Group group)
        {
            if (group is null)
                throw new GroupIsNotInDatabaseException();
        }
        private GroupPermission CreateGroupPermission(uint groupId, uint permissionId)
        {
            return new GroupPermission(permissionId, groupId);
        }

        #endregion
    }
}
