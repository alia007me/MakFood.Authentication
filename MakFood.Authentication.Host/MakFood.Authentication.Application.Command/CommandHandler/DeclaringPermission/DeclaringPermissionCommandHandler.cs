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

namespace MakFood.Authentication.Application.Command.Command.Handler.DeclaringPermission
{
    public class DeclaringPermissionCommandHandler : IRequestHandler<DeclaringPermissionCommand, DeclaringPermissionCommandResponse>
    {
        public IUserRepository _userRepository;
        public IUnitOfWork _unitOfWork;

        public DeclaringPermissionCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DeclaringPermissionCommandResponse> Handle(DeclaringPermissionCommand request, CancellationToken cancellationToken)
        {
            var permissionToAdd = MakePermission(request.Service, request.Method, request.Description);
            var permissionInDatabase = await _userRepository.GetPermissionAsync(permissionToAdd.Service,permissionToAdd.Method,cancellationToken);
            CheckPermission(permissionToAdd, permissionInDatabase);
            var resultOfSave = await _unitOfWork.Commit(cancellationToken);
            
            if(resultOfSave == 0)
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
        private Permission MakePermission(string service , string method , string? description)
        {
            return new Permission(service, method, description , true);
        }
        private void CheckPermission(Permission permissionToAdd , Permission permissionInDatabase)
        {
            if(permissionInDatabase == null)
            {
                _userRepository.AddPermission(permissionToAdd);
            }
            else if(permissionInDatabase != null && permissionInDatabase.IsEnabled == false)
            {
                permissionInDatabase.ChangeEnabledStateToTrue();
            }
            else
            {
                throw new ObjectExistingInDatabaseApplicationException("Permission Is In Database And It Is Working");
            }
        }

        #endregion
    }
}
