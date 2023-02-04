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
    public class WorkSlotConfig : IEntityTypeConfiguration<WorkingSlotTB>
    {
        public void Configure(EntityTypeBuilder<WorkingSlotTB> builder)
        {
            builder.HasOne(x=>x.Lawyer).WithMany(x=>x.WorkingSlot).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.TimeSlot);
        }
    }
}
