using Microsoft.EntityFrameworkCore;
using MultiTenantApp.Models;

namespace MultiTenantApp.DbContexts
{
    public class AppDbContext : DbContext
    {
        private readonly Tenants tenant;

        public AppDbContext(DbContextOptions<AppDbContext> options, Tenants tenant):base(options)
        {
            this.tenant = tenant;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(tenant.ConnectionString);
        }

        public DbSet<Users> Users { get; set; }

    }
}
