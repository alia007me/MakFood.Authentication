using MakFood.FBI.Contracts;
using MakFood.FBI.Middleware;
using MakFood.FBI.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MakFood.FBI
{
    public static class FBIRegistration
    {
        public static IApplicationBuilder UseJwsValidation(this IApplicationBuilder app)
        {
            return app.UseMiddleware<AuthorizationMiddleware>();
        }
        public static IServiceCollection AuthRegister(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<JwsLocalOptions>(config.GetSection("Jws"));
            services.Configure<RedisOptions>(options =>
            {
                options.ConnectionString = config.GetConnectionString("Redis");
            });
            services.AddSingleton<IRedis, Redis>();
            return services;
        }
    }
}
