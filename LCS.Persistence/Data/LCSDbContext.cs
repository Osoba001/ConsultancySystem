using LCS.Domain.Entities;
using LCS.Domain.Response;
using LCS.Persistence.EntityConfig;
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
        public DbSet<LawyerDepartmentTB> LawyerDepartmentTB { get; set; }
        public DbSet<LanguageTB> LanguageTB { get; set; }
        public DbSet<LawyerLanguageTB> LawyerLanguageTB { get; set; }
        public DbSet<TimeSlotTB> TimeSlotTB { get; set; }
        public DbSet<WorkingSlotTB> WorkingSlot { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new AppointmentConfig().Configure(modelBuilder.Entity<AppointmentTB>());
            new ClientConfig().Configure(modelBuilder.Entity<ClientTB>());
            new DepartmentConfig().Configure(modelBuilder.Entity<DepartmentTB>());
            new LanguageConfig().Configure(modelBuilder.Entity<LanguageTB>());
            new LawyerConfig().Configure(modelBuilder.Entity<LawyerTB>());
            new LawyerDepartmentConfig().Configure(modelBuilder.Entity<LawyerDepartmentTB>());
            new LawyerLanguageConfig().Configure(modelBuilder.Entity<LawyerLanguageTB>());
            new WorkSlotConfig().Configure(modelBuilder.Entity<WorkingSlotTB>());
            new TimeSlotConfig().Configure(modelBuilder.Entity<TimeSlotTB>());
            
        }
        public async Task<ActionResult> SaveActionAsync()
        {
            var res = new ActionResult();
            try
            {
                await SaveChangesAsync();
            }
            catch (Exception ex)
            {
                res.AddError(ex.Message);
            }
            return res;
        }
        public async Task<ActionResult<T>> SaveActionAsync<T>(T entity) where T : class 
        {
            var res=new ActionResult<T>();
            try
            {
                await SaveChangesAsync();
                res.Item = entity;
            }
            catch (Exception ex)
            {
                res.AddError(ex.Message);
            }
            return res;
        }
    }
}
