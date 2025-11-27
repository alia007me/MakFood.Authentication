using MakFood.Authentication.Domain.Model.Contracts;
using MakFood.Authentication.Domain.Model.Entities;
using MakFood.Authentication.Domain.Model.Enums;
using MakFood.Authentication.Infraustraucture.Contract;
using MakFood.Authentication.Infraustraucture.Substructure.Base.Extensions;
using MassTransit;
using static MakFood.Authentication.Infraustraucture.Contract.IUnitOfWork;

namespace MakFood.Authentication.Application.Service.Consumers
{
    public class UserRegisteredConsumer : IConsumer<UserRegistered>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UserRegisteredConsumer(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork; 
        }

        public async Task Consume(ConsumeContext<UserRegistered> context)
        {
            var message = context.Message;
            var User = new User(
                username: message.Username,
                password: message.Password.PasswordHasher(),
                gmail: message.Gmail,
                phonenumber: message.PhoneNumber,
                role: Role.Customer
                );
            await _userRepository.AddAsync(User);
            SavingResult result = await _unitOfWork.Commit(context.CancellationToken);

            Console.WriteLine($"[Auth] User registration processed for ID: {message.UserId}. User saved locally.");
        }
    }
}