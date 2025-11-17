using MakFood.Authentication.Domain.Model.Entities;
using MakFood.Authentication.Infraustraucture.Context.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.Authentication.Infraustraucture.Context
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {


        }
        public DbSet<AuthUser> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<GroupPermission> GroupPermissions { get; set; }

        //options.AddInterceptors(serviceProvider.GetRequiredService<AuditSaveChangesInterceptor>());

        protected override void OnModelCreating(ModelBuilder modelBuilder)


        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfigurations).Assembly);
          
            base.OnModelCreating(modelBuilder);
        }

    }
}
