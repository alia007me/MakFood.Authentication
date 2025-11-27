using MakFood.Authentication.Application.Command.Exception;
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

namespace MakFood.Authentication.Application.Command.CommandHandler.DeclaringPermission
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
            var existedPermission = await _permissionRepository.GetPermissionByNameAsync(request.Service,request.Name,cancellationToken);

            if (existedPermission is null)
            {
                _permissionRepository.AddPermission(request.ToModel());
            }
            else if (existedPermission != null)
            {
                existedPermission.ActivatePermission();
            }

            var savingResult = await _unitOfWork.Commit(cancellationToken);
            savingResult.ThrowIfNoChanges<NoPermissionChangedException>();

            return DeclaringPermissionCommandResponse.Succeed;







        }
        #region Private Methods


        #endregion
    }


}
