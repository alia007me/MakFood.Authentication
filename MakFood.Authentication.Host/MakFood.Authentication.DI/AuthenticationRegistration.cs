using FluentValidation;
using MakFood.Authentication.Application.Command.Command.Handler.DeclaringPermission;
using MakFood.Authentication.Domain.Model.Contracts;
using MakFood.Authentication.Infraustraucture.Context;
using MakFood.Authentication.Infraustraucture.Contract;
using MakFood.Authentication.Infraustraucture.Repositories.EF.Repository;
using MakFood.Authentication.Infraustraucture.Substructure.Utils.LocalAccess;
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
        public static IServiceCollection AuthRegistration(this IServiceCollection services, IConfiguration _config)
        {

            services.AddDbContext<AuthDbContext>(options =>
            {
                options.UseSqlServer(_config.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(DeclaringPermissionCommand).Assembly);
            });

            services.AddValidatorsFromAssemblyContaining<DeclaringPermissionCommandValidator>();



            return services;
        }
    }
}
