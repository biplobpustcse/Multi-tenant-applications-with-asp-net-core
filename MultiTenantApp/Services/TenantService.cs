using MultiTenantApp.DbContexts;

namespace MultiTenantApp.Services
{
    public interface ITenantService
    {
       Tenants GetTenantBySubDomain(string subDomain);
       Tenants GetTenantByEmail(string email);
    }

    public class TenantService : ITenantService
    {
        private readonly TenantDbContext tdbc;

        public TenantService(TenantDbContext tdbc)
        {
            this.tdbc = tdbc;
        }

        public Tenants GetTenantByEmail(string email)
        {
            var result = (from tenant in this.tdbc.Tenants
                          join user in this.tdbc.TenantUsers
                          on tenant.CustomerId equals user.CustomerId
                          where user.Email == email
                          select new Tenants
                          {
                              CustomerId = tenant.CustomerId,
                              Customer = tenant.Customer,
                              Host = tenant.Host,
                              SubDomain = tenant.SubDomain,
                              Logo = tenant.Logo,
                              ThemeColor = tenant.ThemeColor,
                              ConnectionString = tenant.ConnectionString
                          }).FirstOrDefault();

            return result;
        }

        public Tenants GetTenantBySubDomain(string subDomain)
        {
            var result = this.tdbc.Tenants.Where(t => t.SubDomain == subDomain).FirstOrDefault();
            return result ?? new Tenants();
        }
    }
}
