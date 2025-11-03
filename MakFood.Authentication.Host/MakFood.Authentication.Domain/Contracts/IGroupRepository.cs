using MakFood.Authentication.Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.Authentication.Domain.Model.Contracts
{
    public interface IGroupRepository
    {
        void AddGroup(Group group);
        Task<Group> GetGroupByIdAsync(uint groupId, CancellationToken ct);
        Task<Group> GetGroupByNameAsync(string groupName, CancellationToken ct);
        Task<GroupPermission> GetGroupPermissionAsync(uint groupId,uint permissionId , CancellationToken ct);
    }
}
