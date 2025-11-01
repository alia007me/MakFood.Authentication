using MakFood.Authentication.Domain.Model.Base;
using MakFood.Authentication.Domain.Model.Enums;
using MakFood.Authentication.Infraustraucture.Substructure.Base.DomainExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.Authentication.Domain.Model.Entities
{
    public class Permission : BaseEntity<uint>
    {

        private Permission() { }
        public Permission(string service, string method, string description)
        {
            CheckServiceName(service);
            CheckMethod(method);
            Service = service;
            Name = method;
            Description = description;
            Status = PermissionStatus.Activated;
        }

        public string Service { get; private set; }
        public string Name { get; private set; }
        public string Key => $"{Service}.{Name}";
        public string? Description { get; private set; }
        public PermissionStatus Status { get; private set; }


        public void ActivatePermission()
        {
            Status = PermissionStatus.Activated;
        }

        public void DeactivatePermission()
        {
            Status = PermissionStatus.Deactivated;
        }
        public void DeletePermission()
        {
            Status = PermissionStatus.Deleted;
        }

        #region Private Methods
        private void CheckServiceName(string serviceName)
        {
            if (string.IsNullOrEmpty(serviceName)) { throw new ValidationFailedDomainException("Name Serivce Can't Be Null"); }
        }
        private void CheckMethod(string method)
        {
            if (string.IsNullOrEmpty(method)) { throw new ValidationFailedDomainException("Method Can't Be Null"); }
        }
        #endregion
    }
}
