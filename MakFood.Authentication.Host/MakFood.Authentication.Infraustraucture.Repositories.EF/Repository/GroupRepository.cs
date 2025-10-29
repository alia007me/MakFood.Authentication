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
        public DbSet<Group> _groups { get; private set; }
        public DbSet<GroupPermission> _groupPermissions { get; private set; }

        public GroupRepository(AuthDbContext groups)
        {
            _groups = groups.Set<Group>();
            _groupPermissions = groups.Set<GroupPermission>();
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
            return await _groupPermissions.SingleOrDefaultAsync(x => x.GroupId == groupId && x.PermissionId == permissionId, ct);
        }
    }
}
