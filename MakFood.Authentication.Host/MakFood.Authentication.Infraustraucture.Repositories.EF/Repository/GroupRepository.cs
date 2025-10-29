using MakFood.Authentication.Domain.Model.Contracts;
using MakFood.Authentication.Domain.Model.Entities;
using MakFood.Authentication.Infraustraucture.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.Authentication.Infraustraucture.Repositories.EF.Repository
{
    public class GroupRepository : IGroupRepository
    {
        private readonly DbSet<Group> _groups;

        public GroupRepository(AuthDbContext groups)
        {
            _groups = groups.Set<Group>();
        }

        public void AddGroup(Group group)
        {
            _groups.Add(group);
        }

        public async Task<Group> GetGroupAsync(string groupName, CancellationToken ct)
        {
            return await _groups.SingleOrDefaultAsync(x => x.GroupName == groupName, ct);
        }

        public async Task<GroupPermission> GetGroupPermissionAsync(uint groupId, uint permissionId, CancellationToken ct)
        {
            return await _groups.Include(x=>x.Permissions).SelectMany(x=>x.Permissions)
                .SingleOrDefaultAsync(x => x.GroupId == groupId && x.PermissionId == permissionId, ct);
        }
    }
}
