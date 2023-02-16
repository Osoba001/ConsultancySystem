using Law.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Law.Persistence.EntityConfig
{
    public class TimeSlotConfig : IEntityTypeConfiguration<TimeSlot>
    {
        public void Configure(EntityTypeBuilder<TimeSlot> builder)
        {
            builder.Property(x => x.StartMinute).IsRequired();
            builder.Property(x => x.EndMinute).IsRequired();
            builder.Property(x => x.StartHour).IsRequired();
            builder.Property(x => x.EndHour).IsRequired();
            builder.HasIndex(x => x.Index).IsUnique();
        }
    }
}
