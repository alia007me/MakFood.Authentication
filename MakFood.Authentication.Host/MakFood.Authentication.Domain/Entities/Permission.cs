using MakFood.Authentication.Domain.Model.Base;
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
            Method = method;
            Description = description;
        }

        public string Service { get; private set; }
        public string Method { get; private set; }
        public string Key => Service + "." + Method;
        public string? Description { get; private set; }


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
