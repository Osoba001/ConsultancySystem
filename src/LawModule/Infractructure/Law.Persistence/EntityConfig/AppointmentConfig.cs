using Law.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Law.Persistence.EntityConfig
{
    internal class AppointmentConfig : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasOne(x => x.Lawyer).WithMany(x => x.Appointments).HasForeignKey(x => x.LawyerId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Client).WithMany(x => x.Appointments).HasForeignKey(x => x.ClientId).OnDelete(DeleteBehavior.NoAction);
            builder.HasIndex(x => new { x.LawyerId, x.ReviewDate }).IsUnique();
            builder.HasIndex(x => new { x.ClientId, x.ReviewDate }).IsUnique();
            builder.Property(x => x.Language);
            builder.HasOne(x => x.TimeSlot);
            builder.Property(x => x.AppointmentType).IsRequired();
            builder.Property(x => x.ReviewDate).IsRequired();
            builder.Property(x => x.Charge).IsRequired();
        }
    }
}
