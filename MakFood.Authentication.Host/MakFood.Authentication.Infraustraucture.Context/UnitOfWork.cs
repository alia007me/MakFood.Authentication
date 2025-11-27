using MakFood.Authentication.Infraustraucture.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MakFood.Authentication.Infraustraucture.Contract.IUnitOfWork;

namespace MakFood.Authentication.Infraustraucture.Context
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AuthDbContext _context;

        public UnitOfWork(AuthDbContext context)
        {
            _context = context;
        }

        public async Task<SavingResult> Commit(CancellationToken ct = default)
        {
            var savedChangedStateCount = await _context.SaveChangesAsync(ct);
            return new SavingResult { ChangesCount = savedChangedStateCount };
        }
    }
}
