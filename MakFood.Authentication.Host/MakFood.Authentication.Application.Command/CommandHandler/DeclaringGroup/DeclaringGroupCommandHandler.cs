using MakFood.Authentication.Application.Command.Exception;
using MakFood.Authentication.Domain.Model.Contracts;
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
    public partial class DeclaringGroupCommandHandler : IRequestHandler<DeclaringGroupCommand, DeclaringGroupCommandResponse>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeclaringGroupCommandHandler(IGroupRepository groupRepository, IUnitOfWork unitOfWork)
        {
            _groupRepository = groupRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DeclaringGroupCommandResponse> Handle(DeclaringGroupCommand command, CancellationToken cancellationToken)
        {
            await CheckForExistence(command.Name, cancellationToken);

            _groupRepository.AddGroup(command.ToModel());

            var savingResult = await _unitOfWork.Commit(cancellationToken);

            savingResult.ThrowIfNoChanges<NoGroupChangedException>();

            return DeclaringGroupCommandResponse.Succeeded;

        }

        private async Task CheckForExistence(string groupName, CancellationToken ct)
        {
            var existingGroup = await _groupRepository.IsGroupExist(groupName, ct);

            if (existingGroup)
                throw new GroupIsInDatabaseException();
        }
    }
}
