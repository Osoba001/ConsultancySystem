using LCS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCS.Persistence.Data
{
    public class LCSDbContext:DbContext
    {
        public LCSDbContext(DbContextOptions<LCSDbContext> options):base(options)
        {

        }
        public DbSet<LawyerTB> LawyerTB { get; set; }
        public DbSet<ClientTB> ClientTB { get; set; }
        public DbSet<AppointmentTB> AppointmentTB { get; set; }
        public DbSet<DepartmentTB> DepartmentTB { get; set; }
        public DbSet<UserTB> UserTB { get; set; }
    }
}
