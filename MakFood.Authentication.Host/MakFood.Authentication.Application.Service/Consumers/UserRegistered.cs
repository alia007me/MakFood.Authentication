using System;
using MakFood.Authentication.Domain.Model.Enums;

namespace MakFood.Authentication.Application.Service.Consumers
{
    public record UserRegistered
    {
        public UserRegistered(
            Guid userId,
            string username,
            string password,
            string phoneNumber,
            string? gmail,
            Role role) 
        {
            UserId = userId;
            Username = username;
            Password = password;
            PhoneNumber = phoneNumber;
            Gmail = gmail;
            Role = role;
        }

        public Guid UserId { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string PhoneNumber { get; private set; }
        public string? Gmail { get; private set; }
        public Role Role { get; private set; }
    }
}