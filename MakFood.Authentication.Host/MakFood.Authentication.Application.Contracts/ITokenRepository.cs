using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.Authentication.Application.Contracts
{
    public interface ITokenRepository
    {
        Task AddAsync(Guid userId, string jti, DateTime expiresAt, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(string jti, CancellationToken cancellationToken = default);
        Task RemoveByJtiAsync(string jti, CancellationToken cancellationToken = default);
        Task RemoveByUserAndJtiAsync(Guid userId, string jti, CancellationToken cancellationToken = default);
    }
}
