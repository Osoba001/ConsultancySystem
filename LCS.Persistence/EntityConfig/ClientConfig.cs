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
    public class ClientConfig:IEntityTypeConfiguration<ClientTB>
    {
        public void Configure(EntityTypeBuilder<ClientTB> builder)
        {
            builder.HasMany(x => x.Appointments).WithOne(x => x.Client);
            builder.Property(x => x.FirstName).HasMaxLength(100);
            builder.Property(x => x.LastName).HasMaxLength(100);
            builder.Property(x => x.PhoneNo).HasMaxLength(25);
        }
    }
}
