using MakFood.Authentication.Infraustraucture.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.Authentication.Infraustraucture.Context
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AuthDbContext _context;

        public UnitOfWork(AuthDbContext context)
        {
            _context = context;
        }

        public async Task<int> Commit(CancellationToken ct)
        {
            return await _context.SaveChangesAsync(ct);
        }
    }
}
