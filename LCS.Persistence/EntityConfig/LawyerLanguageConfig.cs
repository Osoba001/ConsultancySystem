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
    public class LawyerLanguageConfig : IEntityTypeConfiguration<LawyerLanguageTB>
    {
        public void Configure(EntityTypeBuilder<LawyerLanguageTB> builder)
        {
            builder.HasOne(x=>x.Lawyer).WithMany(x=>x.Languages).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Language);
        }
    }
}
