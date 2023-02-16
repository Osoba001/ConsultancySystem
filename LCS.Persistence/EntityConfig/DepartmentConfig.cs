using Law.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Law.Persistence.EntityConfig
{
    internal class DepartmentConfig : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasMany(x => x.Lawyers).WithMany(x => x.Departments);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(250);
            builder.HasIndex(x => x.Name).IsUnique();
        }
    }
}
