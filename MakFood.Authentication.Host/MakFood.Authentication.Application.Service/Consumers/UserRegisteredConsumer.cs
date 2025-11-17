
using MakFood.Authentication.Domain.Model.Contracts;
using MakFood.Authentication.Domain.Model.Entities;
using MakFood.Authentication.Infraustraucture.Contract;
using MassTransit;

namespace MakFood.Authentication.Application.Service.Consumers
{
    public class UserRegisteredConsumer : IConsumer<UserRegistered>
    {
        private readonly IUserRepository _UserRepository;
        private readonly IUnitOfWork _UnitOfWork;

        public UserRegisteredConsumer(IUserRepository authUserRepository)
        {
            _UserRepository = authUserRepository;
        }

        public async Task Consume(ConsumeContext<UserRegistered> context)
        {
            var message = context.Message;

            var authUser = new AuthUser
            {
                Id = message.UserId,
                Username = message.Username,
                PhoneNumber = message.PhoneNumber,
                PasswordHash = message.PasswordHash, 
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            await _UserRepository.AddAsync(authUser);
             _UnitOfWork.Commit();

            Console.WriteLine($"[Auth] User registration processed for ID: {message.UserId}. User saved locally.");
        }
    }
}