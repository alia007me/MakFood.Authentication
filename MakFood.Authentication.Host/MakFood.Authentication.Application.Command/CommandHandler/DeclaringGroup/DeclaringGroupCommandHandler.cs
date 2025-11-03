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

namespace MakFood.Authentication.Application.Command.CommandHandler.DeclaringGroup
{
    public class DeclaringGroupCommandHandler : IRequestHandler<DeclaringGroupCommand, DeclaringGroupCommandResponse>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeclaringGroupCommandHandler(IGroupRepository groupRepository, IUnitOfWork unitOfWork)
        {
            _groupRepository = groupRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DeclaringGroupCommandResponse> Handle(DeclaringGroupCommand request, CancellationToken cancellationToken)
        {
            var newGroup = CreateGroup(request.GroupName, request.Description);
            var existingGroup = await _groupRepository.GetGroupAsync(newGroup.GroupName ,cancellationToken);

            if (existingGroup != null)
                throw new ObjectExistingInDatabaseApplicationException("This Group With This Name Is In Database");

            _groupRepository.AddGroup(newGroup);
            var savingResult = await _unitOfWork.Commit(cancellationToken);

            if (savingResult == 0) 
                throw new OperationFailedApplicationException("Nothing Added To The DataBase");

            var respone = new DeclaringGroupCommandResponse() { Success = true };
            return respone;

        }



        #region Private Methods
        private Group CreateGroup(string groupName, string description)
        {
            return new Group(groupName, description);
        }

        #endregion
    }
}
