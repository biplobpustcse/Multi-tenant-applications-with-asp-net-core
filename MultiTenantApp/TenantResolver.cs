using MultiTenantApp.Services;
using SaasKit.Multitenancy;
using System.Security.Claims;

namespace MultiTenantApp
{
    public interface ITenantResolver
    {
        Task<TenantContext<Tenants>> ResolveAsync(HttpContext context);
    }

    public class TenantResolver : ITenantResolver<Tenants>
    {
        private readonly IConfiguration configuration;
        // Gets or sets the current HttpContext. Returns null if there is no active HttpContext.
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ITenantService tenantService;

        public TenantResolver(IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor,
            ITenantService tenantService)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.tenantService = tenantService;
            this.configuration = configuration;
        }

        public async Task<TenantContext<Tenants>> ResolveAsync(HttpContext context)
        {   // get sub-domain form browser current url. if sub-domain is not exists then will set empty string
            string subDomainFromUrl = context.Request.Host.Value.ToLower().Split(".")[0] ?? string.Empty;
            // checking has any tenant by current sub-domain. 
            var result = this.tenantService.GetTenantBySubDomain(subDomainFromUrl);
            Tenants tenant = new();
            // checking has any subdomain is exists in current url
            if (!string.IsNullOrEmpty(result.SubDomain))
            {
                // checking orginal sub-domain and current url sub-domain
                if (!result.SubDomain.Equals(subDomainFromUrl)) return null; // if sub-domain is different then return null
                else
                {
                    tenant.CustomerId = result.CustomerId;
                    tenant.Customer = result.Customer;
                    tenant.Host = result.Host;
                    tenant.SubDomain = result.SubDomain;
                    tenant.Logo = result.Logo;
                    tenant.ThemeColor = result.ThemeColor;
                    tenant.ConnectionString = result.ConnectionString;
                    return await Task.FromResult(new TenantContext<Tenants>(tenant));
                }
            }
            else return await Task.FromResult(new TenantContext<Tenants>(tenant));

        }
    }
}
