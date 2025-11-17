namespace MakFood.Authentication.Application.Service.Consumers
{
    public record UserRegistered
    {
        public Guid UserId { get; init; }
        public string Username { get; init; }
        public string PhoneNumber { get; init; }
        public string PasswordHash { get; init; } 
    }
}