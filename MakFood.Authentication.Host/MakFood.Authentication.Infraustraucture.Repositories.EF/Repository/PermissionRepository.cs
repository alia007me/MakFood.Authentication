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
    public class PermissionRepository : IPermissionRepository
    {
        private readonly AuthDbContext _context;

        public PermissionRepository(AuthDbContext context)
        {
            _context = context;
        }

        public void AddPermission(Permission permission)
        {
            _context.Permissions.Add(permission);
        }

        public async Task<Permission> GetPermissionByIdAsync(uint permissionId, CancellationToken ct)
        {
            return await _context.Permissions.SingleOrDefaultAsync(x => x.Id == permissionId, ct);
        }

        public async Task<Permission> GetPermissionByNameAsync(string service, string name, CancellationToken ct)
        {
            return await _context.Permissions.SingleOrDefaultAsync(x=>x.Service.ToLower() == service.ToLower() && x.Name == name.ToLower(), ct);
        }
    }
}
