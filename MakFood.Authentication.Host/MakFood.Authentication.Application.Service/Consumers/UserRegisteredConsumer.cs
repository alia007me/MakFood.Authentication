using MakFood.Authentication.Domain.Model.Contracts;
using MakFood.Authentication.Domain.Model.Entities;
using MakFood.Authentication.Infraustraucture.Contract;
using MassTransit;
using MakFood.Authentication.Domain.Model.Enums;
using MakFood.Authentication.Infraustraucture.Substructure.Base.Extensions;

namespace MakFood.Authentication.Application.Service.Consumers
{
    public class UserRegisteredConsumer : IConsumer<UserRegistered>
    {
        private readonly IUserRepository _UserRepository;
        private readonly IUnitOfWork _UnitOfWork;
        public UserRegisteredConsumer(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _UserRepository = userRepository;
            _UnitOfWork = unitOfWork; 
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
            await _UserRepository.AddAsync(User);
            await _UnitOfWork.Commit();

            Console.WriteLine($"[Auth] User registration processed for ID: {message.UserId}. User saved locally.");
        }
    }
}