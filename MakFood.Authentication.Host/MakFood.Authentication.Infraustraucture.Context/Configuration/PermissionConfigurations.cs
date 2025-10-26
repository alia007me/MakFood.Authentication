using MakFood.Authentication.Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.Authentication.Infraustraucture.Context.Configuration
{
    public class PermissionConfigurations : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();
            builder.Property(x => x.Service)
                .IsRequired();

            builder.Property(x => x.Method)
                .IsRequired();

            builder.Ignore(x => x.Key);

            builder.Property(x => x.ComputedKey)
                .HasComputedColumnSql("[Service]+'.'+[Method]");






        }
    }
}
