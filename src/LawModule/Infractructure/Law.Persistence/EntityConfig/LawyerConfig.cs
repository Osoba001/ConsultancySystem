using Law.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace LCS.Persistence.EntityConfig;

public class LawyerConfig : IEntityTypeConfiguration<Lawyer>
{
    public void Configure(EntityTypeBuilder<Lawyer> builder)
    {
        builder.HasMany(x => x.Appointments).WithOne(x => x.Lawyer);
        builder.HasMany(x => x.OnlineWorkingSlots);
        builder.Property(x => x.Languages).HasConversion(
            v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
            v => JsonSerializer.Deserialize<List<string>>(v,(JsonSerializerOptions)null));
        builder.HasMany(x => x.Departments).WithMany(x=>x.Lawyers);
        builder.Property(x=>x.FirstName).HasMaxLength(100);
        builder.Property(x=>x.PhoneNo).HasMaxLength(25);
        builder.HasQueryFilter(x => x.IsDelete == false);
    }
}
