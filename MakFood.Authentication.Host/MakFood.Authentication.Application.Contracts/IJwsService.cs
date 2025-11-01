using MakFood.Authentication.Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.Authentication.Application.Contracts
{
    public interface IJwsService
    {
        Task<string> CreateJwsToken(User user , CancellationToken cancellationToken);
        Task DeleteJwsToken(User user, string jwsToken);

    }
}
