using LCS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCS.Persistence.EntityConfig
{
    public class UserConfig: IEntityTypeConfiguration<UserTB>
    {
        public void Configure(EntityTypeBuilder<UserTB> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Role).IsRequired();
            builder.Property(x => x.HashPassword).IsRequired();
            builder.Property(x => x.SaltPassword).IsRequired();
        }
    }
}
