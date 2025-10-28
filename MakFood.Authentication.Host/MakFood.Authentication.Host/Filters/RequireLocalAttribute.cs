using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Cryptography.X509Certificates;

namespace MakFood.Authentication.Host.Filters
{
    public class RequireLocalAttribute : Attribute, IAsyncActionFilter
    {
        private readonly string _allowedHost = "localhost";
        private readonly int _allowedPort = 7127;


        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var request = context.HttpContext.Request;
            var host= request.Host.Host;
            var port = request.Host.Port;

            if(!string.Equals(host,_allowedHost, StringComparison.OrdinalIgnoreCase) || _allowedPort != port)
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
