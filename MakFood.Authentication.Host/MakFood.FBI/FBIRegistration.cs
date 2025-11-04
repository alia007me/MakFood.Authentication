using MakFood.FBI.Middleware;
using MakFood.FBI.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.FBI
{
    public static class FBIRegistration
    {
        public static IApplicationBuilder UseJwsValidation(this IApplicationBuilder app)
        {
            return app.UseMiddleware<AuthorizationMiddleware>();
        }
        public static IServiceCollection AuthRegister(this IServiceCollection services , IConfiguration config)
        {
            services.Configure<JwsLocalOptions>(config.GetSection("Jws"));
            return services;
        }
    }
}
