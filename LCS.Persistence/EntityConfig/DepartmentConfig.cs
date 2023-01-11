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
    internal class DepartmentConfig: IEntityTypeConfiguration<DepartmentTB>
    {
        public void Configure(EntityTypeBuilder<DepartmentTB> builder)
        {
            builder.HasMany(x => x.Lawyers).WithMany(x => x.Departments);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(250);
            builder.HasIndex(x => x.Name).IsUnique();
        }
    }
}
