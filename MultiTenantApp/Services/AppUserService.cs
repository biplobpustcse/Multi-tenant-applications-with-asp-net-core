using Microsoft.AspNetCore.Http.Extensions;
using MultiTenantApp.DbContexts;
using MultiTenantApp.Models;

namespace MultiTenantApp.Services
{
    public interface IAppUserService
    {
        public string GetTenantByEmail(string email);
        public string Signin(Signin model);
    }
    public class AppUserService : IAppUserService
    {
        private readonly AppDbContext adbc;
        private readonly ITenantService tenantService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AppUserService(AppDbContext adbc, ITenantService tenantService, IHttpContextAccessor httpContextAccessor)
        {
            this.adbc = adbc;
            this.tenantService = tenantService;
            this.httpContextAccessor = httpContextAccessor;
        }

        public string GetTenantByEmail(string email)
        {
            var tenant = this.tenantService.GetTenantByEmail(email);
            if (tenant is not null)
            {
                // get current url information
                string newUrl = string.Empty;
                // get current sceme or protocol 
                string scheme = this.httpContextAccessor.HttpContext.Request.Scheme;
                newUrl += scheme + "://" + tenant.SubDomain + "." + tenant.Host + "/home/signin";
                return newUrl.ToLower();
            }
            else return null;
        }

        public string Signin(Signin model)
        {
            var result = this.adbc.Users.FirstOrDefault(x => x.UserEmail == model.Email && x.Password == model.Password);
            if (result is not null)
            {
                // get current sceme or protocol 
                string getDisplayUrl = this.httpContextAccessor.HttpContext.Request.GetDisplayUrl();
                return $"{getDisplayUrl}home/index".ToLower();
            }
            return null;
        }
    }
}
