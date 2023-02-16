using Law.Domain.Models;
using Law.Persistence.EntityConfig;
using LCS.Persistence.EntityConfig;
using Microsoft.EntityFrameworkCore;
using Utilities.ActionResponse;

namespace Law.Persistence.Data
{
    public class LCSDbContext : DbContext
    {

        public LCSDbContext(DbContextOptions<LCSDbContext> options) : base(options)
        {
        }
        public DbSet<Lawyer> LawyerTB { get; set; }
        public DbSet<Client> ClientTB { get; set; }
        public DbSet<Appointment> AppointmentTB { get; set; }
        public DbSet<Department> DepartmentTB { get; set; }
        public DbSet<TimeSlot> TimeSlotTB { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new AppointmentConfig().Configure(modelBuilder.Entity<Appointment>());
            new ClientConfig().Configure(modelBuilder.Entity<Client>());
            new DepartmentConfig().Configure(modelBuilder.Entity<Department>());
            new LawyerConfig().Configure(modelBuilder.Entity<Lawyer>());
            new TimeSlotConfig().Configure(modelBuilder.Entity<TimeSlot>());

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
            var res = new ActionResult<T>();
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
