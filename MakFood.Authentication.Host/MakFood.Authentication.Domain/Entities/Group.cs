using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.Authentication.Domain.Model.Entities
{
    public class Group
    {
        private Group() { }
        public Group(string groupName, string description)
        {
            CheckGroupName(groupName);

            GroupName = groupName;
            Description = description;
        }

        public uint GroupId { get; private set; }
        public string GroupName { get; private set; }
        public string Description { get; private set; }

        public IList<GroupPermission> Permissions { get; private set; } = new List<GroupPermission>();


        #region Private Methods
        private void CheckGroupName(string groupName)
        {
            if (string.IsNullOrEmpty(groupName)) { throw new ArgumentNullException("GroupName Can't Be Null"); }
        }
        #endregion
    }
}
