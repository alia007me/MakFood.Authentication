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
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Username)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(x => x.Password)
                .IsRequired();

            builder.Property(x => x.Phonenumber)
                .IsRequired()
                .HasMaxLength(11);

            builder.Property(x => x.Role)
                .HasConversion<string>()
                .IsRequired();

            builder.HasMany(x => x.Groups)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);





        }
    }
}
