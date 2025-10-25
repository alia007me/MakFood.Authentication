using MakFood.Authentication.Domain.Model.Base;
using MakFood.Authentication.Infraustraucture.Substructure.Base.DomainExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.Authentication.Domain.Model.Entities
{
    public class Group : BaseEntity<Guid>
    {
        private Group() { }

        public Group(string groupName, string description)
        {
            CheckGroupName(groupName);
            Id = Guid.NewGuid();
            GroupName = groupName;
            Description = description;
        }
        private readonly List<GroupPermission> _permissions = new List<GroupPermission>();


        public string GroupName { get; private set; }
        public string Description { get; private set; }

        public IEnumerable<GroupPermission> Permissions => _permissions.AsReadOnly();


        public void AddPermissionsToGroup(GroupPermission permission)
        {
            foreach (var item in _permissions)
            {
                if (item == permission)
                    throw new ValidationFailedDomainException("This Permission Exists In This Group");
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
