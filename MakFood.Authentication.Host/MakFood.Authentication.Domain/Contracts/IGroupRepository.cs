using MakFood.Authentication.Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.Authentication.Domain.Model.Contracts
{
    public interface IGroupRepository
    {
        void AddGroup(Group group);

        /// <summary>
        /// بررسی وجود یک گروه دسترسی با استفاده از نام
        /// </summary>
        /// <param name="name">نام گروه دسترسی</param>
        /// <param name="ct">توکن کنسل کردن عملیات</param>
        /// <returns>وجود گروه</returns>
        Task<bool> IsGroupExist(string name, CancellationToken ct);

        Task<bool> IsGroupPermissionExist(uint groupId, uint permissionId, CancellationToken ct);


        Task<Group> GetGroupAsync(string groupName , CancellationToken ct);
        Task<GroupPermission> GetGroupPermissionAsync(uint groupId,uint permissionId , CancellationToken ct);
    }
}
