using MakFood.Authentication.Infraustraucture.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.Authentication.DI
{
    public static class AuthenticationRegistration
    {
        public static IServiceCollection AuthRegistration(this IServiceCollection services)
        {

            services.AddDbContext<AuthDbContext>(options =>
            {
                options.UseSqlServer("Server=.;Database=MakFood.Authentication;Trusted_Connection=True;TrustServerCertificate=True");
            });


            return services;
        }
    }
}
