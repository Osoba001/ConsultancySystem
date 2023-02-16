using Microsoft.EntityFrameworkCore;
using User.Application.Entities;
using User.Persistence.Data.EntityConfiguration;
using Utilities.ActionResponse;

namespace User.Persistence.Data
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {

        }
        public DbSet<UserTb> UserTb { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new UserConfig().Configure(modelBuilder.Entity<UserTb>());
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
