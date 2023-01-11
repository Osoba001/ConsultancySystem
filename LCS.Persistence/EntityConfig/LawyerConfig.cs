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
    public class LawyerConfig : IEntityTypeConfiguration<LawyerTB>
    {
        public void Configure(EntityTypeBuilder<LawyerTB> builder)
        {
            builder.HasMany(x => x.Departments).WithMany(x => x.Lawyers);
            builder.HasMany(x => x.Appointments).WithOne(x => x.Lawyer);
            builder.Property(x=>x.User).IsRequired();
            builder.Property(x=>x.FirstName).HasMaxLength(100);
            builder.Property(x=>x.LastName).HasMaxLength(100);
            builder.Property(x=>x.PhoneNo).HasMaxLength(25);
        }
    }
}
