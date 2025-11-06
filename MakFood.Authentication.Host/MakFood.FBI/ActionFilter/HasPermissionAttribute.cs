using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.FBI.ActionFilter
{
    public class HasPermissionAttribute : Attribute, IAsyncAuthorizationFilter
    {
        private readonly string _permission;
        public HasPermissionAttribute(string permission)
        {
            _permission = permission;
        }



        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (user.Identity is null || !user.Identity.IsAuthenticated)
            {
                context.Result = new JsonResult(new
                {
                    success = false,
                    message = "Unauthorized"
                })
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
                return;
            }
            var hasPermission = user.Claims.Any(x => x.Type == "permission" && x.Value == _permission);
            if (!hasPermission)
            {
                context.Result = new JsonResult(new
                {
                    success = false,
                    message = "Forbidden"
                })
                {
                    StatusCode = StatusCodes.Status403Forbidden
                };
                return;
            }
            await Task.CompletedTask;
        }
    }
}
