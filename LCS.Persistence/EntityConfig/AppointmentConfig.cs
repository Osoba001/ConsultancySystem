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
    internal class AppointmentConfig: IEntityTypeConfiguration<AppointmentTB>
    {
        public void Configure(EntityTypeBuilder<AppointmentTB> builder)
        {
            builder.HasOne(x => x.Lawyer).WithMany(x => x.Appointments).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(x => x.Client).WithMany(x => x.Appointments).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Language);
            builder.HasOne(x => x.TimeSlot);
            builder.HasOne(x => x.TimeSlot);
            builder.Property(x => x.AppointmentType).IsRequired();
            builder.Property(x => x.ReviewDate).IsRequired();
            builder.Property(x => x.Charge).IsRequired();
        }
    }
}
