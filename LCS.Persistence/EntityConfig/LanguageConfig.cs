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
    public class LanguageConfig : IEntityTypeConfiguration<LanguageTB>
    {
        public void Configure(EntityTypeBuilder<LanguageTB> builder)
        {
            builder.HasIndex(x => x.Name).IsUnique();
            builder.Property(x=>x.Name).IsRequired().HasMaxLength(100);
        }
    }
}
