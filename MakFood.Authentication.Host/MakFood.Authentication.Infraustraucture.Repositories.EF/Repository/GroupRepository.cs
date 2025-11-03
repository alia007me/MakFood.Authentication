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

        public async Task<Group> GetGroupByIdAsync(uint groupId, CancellationToken ct)
        {
            return await _groups.SingleOrDefaultAsync(x => x.Id == groupId, ct);
        }

        public async Task<Group> GetGroupByNameAsync(string groupName, CancellationToken ct)
        {
            return await _groups.SingleOrDefaultAsync(x => x.GroupName.ToLower() == groupName.ToLower() , ct);
        }

        public async Task<bool> IsGroupExist(string name, CancellationToken ct)
            => await _groups.AnyAsync(c => c.GroupName == name, ct);

        public async Task<bool> IsGroupPermissionExist(uint groupId , uint permissionId, CancellationToken ct)
            => await _groups.Include(c=>c.Permissions).SelectMany(x=>x.Permissions).AnyAsync(x=>x.GroupId == groupId && x.PermissionId == permissionId, ct);




    }
}
