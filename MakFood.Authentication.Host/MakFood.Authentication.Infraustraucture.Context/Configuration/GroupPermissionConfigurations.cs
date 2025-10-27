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
    public class GroupPermissionConfigurations : IEntityTypeConfiguration<GroupPermission>
    {
        public void Configure(EntityTypeBuilder<GroupPermission> builder)
        {

            builder.HasKey(x => new { x.GroupId, x.PermissionId });

            builder.Property(c => c.Id).ValueGeneratedOnAdd();



        }
    }
}
