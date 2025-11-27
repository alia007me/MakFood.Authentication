using FluentValidation;
using MakFood.Authentication.Application.Command.CommandHandler.DeclaringPermission;
using MakFood.Authentication.Application.Contracts;
using MakFood.Authentication.Application.Service.JwsService;
using MakFood.Authentication.Domain.Model.Contracts;
using MakFood.Authentication.Infraustraucture.Context;
using MakFood.Authentication.Infraustraucture.Context.Redis;
using MakFood.Authentication.Infraustraucture.Contract;
using MakFood.Authentication.Infraustraucture.Repositories.EF.Repository;
using MakFood.Authentication.Infraustraucture.Substructure.Utils.JwsInformation;
using MakFood.Authentication.Infraustraucture.Substructure.Utils.LocalAccess;
using MakFood.FBI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
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
            services.AddScoped<IJwsService, JwsService>();
            services.AddSingleton<IRedisCache,RedisCache>();
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(DeclaringPermissionCommand).Assembly);
            });

            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var configuration = _config.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(configuration);
            });
            

            services.AddValidatorsFromAssemblyContaining<DeclaringPermissionCommandValidator>();
            services.Configure<JwsInformationOptions>(_config.GetSection("Jws"));
            services.AuthRegister(_config);


            return services;
        }
    }
}
