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
    public class LawyerDepartmentConfig : IEntityTypeConfiguration<LawyerDepartmentTB>
    {
        public void Configure(EntityTypeBuilder<LawyerDepartmentTB> builder)
        {
            builder.HasOne(x=>x.Department);
            builder.HasOne(x => x.Lawyer).WithMany(x => x.JoinedDepartments).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
