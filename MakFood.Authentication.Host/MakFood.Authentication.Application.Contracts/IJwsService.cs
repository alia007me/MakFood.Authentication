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
        Task<string> CreateJWSToken(User user , CancellationToken cancellationToken);
    }
}
