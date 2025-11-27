using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MakFood.KGB.Auditing
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAuditLogging(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AuditLoggingDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("AuditConnection")
                                         ?? configuration.GetConnectionString("DefaultConnection")));

            services.AddHttpContextAccessor();

            services.AddScoped<AuditSaveChangesInterceptor>();


            return services;
        }
    }
}