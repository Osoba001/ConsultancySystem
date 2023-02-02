using Auth.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Data.EntityConfiguration
{
    public class UserConfig : IEntityTypeConfiguration<UserTb>
    {
        public void Configure(EntityTypeBuilder<UserTb> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.AssignedUserRoles).WithOne(x => x.User);
            builder.Property(x=> x.Email).IsRequired().HasMaxLength(300);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(300);
            builder.HasIndex(x => x.Email).IsUnique();
            builder.Property(x => x.PasswordHash).IsRequired();
            builder.Property(x => x.PasswordSalt).IsRequired();
            builder.HasQueryFilter(x => x.IsDeleted==false);
        }
    }
}
