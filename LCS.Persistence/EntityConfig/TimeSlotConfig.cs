using LCS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LCS.Persistence.EntityConfig
{
    public class TimeSlotConfig : IEntityTypeConfiguration<TimeSlotTB>
    {
        public void Configure(EntityTypeBuilder<TimeSlotTB> builder)
        {
            builder.Property(x => x.StartMinute).IsRequired();
            builder.Property(x=>x.EndMinute).IsRequired();
            builder.Property(x=>x.StartHour).IsRequired();
            builder.Property(x=>x.EndHour).IsRequired();
            builder.HasIndex(x=>x.Index).IsUnique();
        }
    }
}
