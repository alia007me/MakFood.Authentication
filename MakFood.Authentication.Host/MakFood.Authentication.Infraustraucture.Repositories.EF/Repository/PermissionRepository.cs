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

        public async Task<Permission> GetPermissionAsync(string service ,  string method , CancellationToken ct)
        {
            return await _context.Permissions.SingleOrDefaultAsync(x => x.Service == service && x.Method == method, ct);
        }
    }
}
