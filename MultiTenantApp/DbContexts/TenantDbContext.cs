using Microsoft.EntityFrameworkCore;

namespace MultiTenantApp.DbContexts
{
    public class TenantDbContext : DbContext
    {
        public TenantDbContext(DbContextOptions<TenantDbContext> option) : base(option)
        {
        }

        public DbSet<Tenants> Tenants { get; set; }
        public DbSet<TenantUsers> TenantUsers { get; set; }

    }
}
