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

namespace MakFood.Authentication.Application.Command.CommandHandler.AssignPermissionToGroup
{
    public class AssignPermissionToGroupCommandHandler : IRequestHandler<AssignPermissionToGroupCommand, AssignPermissionToGroupCommandResponse>
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
            var group = await _groupRepository.GetGroupAsync(request.GroupName, cancellationToken);
            if (group == null)
                throw new ObjectNotFoundApplicationException("Group Not Found !");

            var permission = await _permissionRepository.GetPermissionAsync(request.Service, request.Name, cancellationToken);
            if (permission == null)
                throw new ObjectNotFoundApplicationException("Permission Not Found !");

            var existingGroupPermission = _groupRepository.GetGroupPermissionAsync(group.Id, permission.Id, cancellationToken);
            if (existingGroupPermission != null)
                throw new ObjectExistingInDatabaseApplicationException("This Permission Already Assigned To This Group");


            var groupPermission = CreateGroupPermission(group.Id, permission.Id);
            group.AddPermissionsToGroup(groupPermission);

            var savingResult = await _unitOfWork.Commit(cancellationToken);

            if (savingResult == 0)
                throw new OperationFailedApplicationException("Nothing Added To The DataBase");

            var response = new AssignPermissionToGroupCommandResponse() { Success = true };
            return response;




        }
        #region Private Methods
        private GroupPermission CreateGroupPermission(uint groupId, uint permissionId)
        {
            return new GroupPermission(permissionId, groupId);
        }
        #endregion
    }
}
