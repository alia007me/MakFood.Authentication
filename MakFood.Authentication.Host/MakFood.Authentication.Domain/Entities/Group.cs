using MakFood.Authentication.Domain.Model.Base;
using MakFood.Authentication.Infraustraucture.Substructure.Base.DomainExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.Authentication.Domain.Model.Entities
{
    public class Group : BaseEntity<uint>
    {
        private Group() { }
        private static uint _lastId = 0;

        public Group(string groupName, string description)
        {
            CheckGroupName(groupName);
            Id += _lastId;
            GroupName = groupName;
            Description = description;
        }
        private readonly List<GroupPermission> _permissions = new List<GroupPermission>();


        public string GroupName { get; private set; }
        public string Description { get; private set; }

        public IEnumerable<GroupPermission> Permissions => _permissions.AsReadOnly();


        public void AddPermissionsToGroup(GroupPermission permission)
        {
            if (_permissions.Any(c => c == permission))
            {
                throw new ValidationFailedDomainException("This Group Has This Permission");
            }
                _permissions.Add(permission);
        }


        #region Private Methods
        private void CheckGroupName(string groupName)
        {
            if (string.IsNullOrEmpty(groupName)) { throw new ValidationFailedDomainException("GroupName Can't Be Null"); }
        }
        #endregion
    }
}
