using MakFood.Authentication.Infraustraucture.Substructure.Utils.LocalAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System.Security.Cryptography.X509Certificates;

namespace MakFood.Authentication.Host.Filters
{
    public class RequireLocalAttribute : TypeFilterAttribute
    {
        public RequireLocalAttribute() : base(typeof(RequireLocalFilter)) { }
    }

    public class RequireLocalFilter : IAsyncActionFilter
    {
        private readonly LocalAccessOptions _local;

        public RequireLocalFilter(IOptions<LocalAccessOptions> local)
        {
            _local = local.Value;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var request = context.HttpContext.Request;
            var host= request.Host.Host;
            var port = request.Host.Port;

            if(!string.Equals(host,_local.AllowedHost, StringComparison.OrdinalIgnoreCase) || _local.AllowedPort != port)
            {
                context.Result = new JsonResult(new
                {
                    success = false,
                    message = "NotAllowed"
                })
                {
                    StatusCode = StatusCodes.Status403Forbidden
                };
                return;
            }

            await next();   


        }
    }
}
