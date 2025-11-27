using MakFood.Authentication.Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.Authentication.Domain.Model.Contracts
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task<User> GetUserByIdAsync(Guid Id, CancellationToken ct);
        Task<UserGroup> GetUserGroupAsync(Guid userId, uint groupId, CancellationToken ct);
        Task<bool> IsUserGroupExist(Guid userId, uint groupId, CancellationToken ct);
        Task<List<UserGroup>> GetAllUserGroupsAsync(Guid userId, CancellationToken ct);



    }
}
