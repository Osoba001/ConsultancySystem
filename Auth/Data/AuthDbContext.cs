using Auth.Data.EntityConfiguration;
using Auth.Entities;
using Auth.Response;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Data
{
    public class AuthDbContext:DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options):base(options)
        {

        }
        public DbSet<UserTb> UserTb { get; set; }
        public DbSet<UserRoleTb> RoleTb { get; set; }
        public DbSet<AssignedUserRoleTb> AssignedUserRoleTb { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new UserConfig().Configure(modelBuilder.Entity<UserTb>());
            new UserRoleConfig().Configure(modelBuilder.Entity<UserRoleTb>());
            new AssignedUserRoleConfig().Configure(modelBuilder.Entity<AssignedUserRoleTb>());
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
