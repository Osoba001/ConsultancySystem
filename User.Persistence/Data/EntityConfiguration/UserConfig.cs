using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Application.Entities;

namespace User.Persistence.Data.EntityConfiguration
{
    public class UserConfig : IEntityTypeConfiguration<UserTb>
    {
        public void Configure(EntityTypeBuilder<UserTb> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(300);
            builder.Property(x => x.Role).IsRequired();
            builder.Property(x => x.DOB).IsRequired();
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(300);
            builder.HasIndex(x => x.Email).IsUnique();
            builder.Property(x => x.PasswordHash).IsRequired();
            builder.Property(x => x.PasswordSalt).IsRequired();
            builder.HasQueryFilter(x => x.IsDeleted == false);
        }
    }
}
