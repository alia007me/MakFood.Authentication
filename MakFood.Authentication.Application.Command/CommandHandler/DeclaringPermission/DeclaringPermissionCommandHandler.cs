using MakFood.Authentication.Domain.Model.Contracts;
using MakFood.Authentication.Domain.Model.Entities;
using MakFood.Authentication.Domain.Model.Enums;
using MakFood.Authentication.Infraustraucture.Contract;
using MakFood.Authentication.Infraustraucture.Substructure.Base.ApplicationException;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.Authentication.Application.Command.Command.Handler.DeclaringPermission
{
    public class DeclaringPermissionCommandHandler : IRequestHandler<DeclaringPermissionCommand, DeclaringPermissionCommandResponse>
    {
        public IPermissionRepository _permissionRepository;
        public IUnitOfWork _unitOfWork;

        public DeclaringPermissionCommandHandler(IPermissionRepository permissionRepository, IUnitOfWork unitOfWork)
        {
            _permissionRepository = permissionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DeclaringPermissionCommandResponse> Handle(DeclaringPermissionCommand request, CancellationToken cancellationToken)
        {
            var newPermission = CreatePermission(request.Service, request.Method, request.Description);
            var existedPermission = await _permissionRepository.GetPermissionAsync(newPermission.Service,newPermission.Method,cancellationToken);

            if (existedPermission == null)
            {
                _permissionRepository.AddPermission(newPermission);
            }
            else if (existedPermission != null && existedPermission.Status == PermissionStatus.Deactivated)
            {
                existedPermission.ActivatePermission();
            }
            else
            {
                throw new ObjectExistingInDatabaseApplicationException("Permission Is In Database And It Is Working");
            }


            var savingResult = await _unitOfWork.Commit(cancellationToken);
            
            if(savingResult == 0)
            {
                throw new OperationFailedApplicationException("Nothing Added To The DataBase");
            }

            var response = new DeclaringPermissionCommandResponse()
            {
                Success = true,
            };

            return response;




        }
        #region Private Methods
        private Permission CreatePermission(string service , string method , string? description)
        {
            return new Permission(service, method, description , PermissionStatus.Activated);
        }


        #endregion
    }
}
