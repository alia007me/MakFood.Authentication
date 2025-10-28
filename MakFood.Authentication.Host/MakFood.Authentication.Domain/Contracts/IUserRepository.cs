using MakFood.Authentication.Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.Authentication.Domain.Model.Contracts
{
    public interface IUserRepository
    {
        void AddPermission(Permission permission);
        Task<Permission> GetPermissionAsync(string service, string method, CancellationToken ct);

    }
}
