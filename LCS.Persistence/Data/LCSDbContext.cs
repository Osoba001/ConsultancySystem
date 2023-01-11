using LCS.Domain.Entities;
using LCS.Domain.Response;
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
        private readonly DbContextOptions<LCSDbContext> options;

        public LCSDbContext(DbContextOptions<LCSDbContext> options):base(options)
        {
            this.options = options;
        }
        public DbSet<LawyerTB> LawyerTB { get; set; }
        public DbSet<ClientTB> ClientTB { get; set; }
        public DbSet<AppointmentTB> AppointmentTB { get; set; }
        public DbSet<DepartmentTB> DepartmentTB { get; set; }
        public DbSet<UserTB> UserTB { get; set; }

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
        public async Task<ActionResult<T>> SaveActionAsync<T>(T entity)
        {
            var res=new ActionResult<T>();
            try
            {
                await SaveChangesAsync();
                res.Entity = entity;
            }
            catch (Exception ex)
            {
                res.AddError(ex.Message);
            }
            return res;
        }
    }
}
