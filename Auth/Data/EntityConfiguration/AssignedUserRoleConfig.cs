using Auth.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Data.EntityConfiguration
{
    public class AssignedUserRoleConfig : IEntityTypeConfiguration<AssignedUserRoleTb>
    {
        public void Configure(EntityTypeBuilder<AssignedUserRoleTb> builder)
        {
            builder.HasOne(x => x.User).WithMany(x => x.AssignedUserRoles);
            builder.HasOne(x => x.Role).WithMany(x => x.AssignedUserRoles);
        }
    }
}
