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
    public class UserRepository : IUserRepository
    {
        private readonly DbSet<User> _users;
        private readonly AuthDbContext _context;



        public UserRepository(AuthDbContext users, AuthDbContext authDbContext)
        {
            _users = users.Set<User>();
            _context = authDbContext;
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<User> GetUserByIdAsync(Guid Id, CancellationToken ct)
        {
            return await _users.SingleOrDefaultAsync(x=>x.Id == Id,ct);
        }

        public async Task<UserGroup> GetUserGroupAsync(Guid userId, uint groupId, CancellationToken ct)
        {
            return await _users.Include(x => x.Groups).SelectMany(x => x.Groups).SingleOrDefaultAsync(x => x.UserId == userId && x.GroupId == groupId, ct);
        }

        public async Task<bool> IsUserGroupExist(Guid userId, uint groupId, CancellationToken ct)
        {
            return await _users.Include(x => x.Groups).SelectMany(c => c.Groups).AnyAsync(x => x.GroupId == groupId && x.UserId == userId, ct);
        }

        public async Task<List<UserGroup>> GetAllUserGroupsAsync(Guid userId, CancellationToken ct)
        {
            return await _users.Include(x => x.Groups).SelectMany(x => x.Groups).Where(x => x.UserId == userId).ToListAsync(ct);
        }
    }
}
