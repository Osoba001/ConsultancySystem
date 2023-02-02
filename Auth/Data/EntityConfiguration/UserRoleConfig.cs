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
    public class UserRoleConfig : IEntityTypeConfiguration<UserRoleTb>
    {
        public void Configure(EntityTypeBuilder<UserRoleTb> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.AssignedUserRoles).WithOne(x => x.Role);
            builder.Property(x=>x.Name).IsRequired();
            builder.HasIndex(x => x.Name).IsUnique();
        }
    }
}
