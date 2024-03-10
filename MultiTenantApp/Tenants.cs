using System.ComponentModel.DataAnnotations;

namespace MultiTenantApp
{
    public class Tenants
    {
        [Key]
        public int CustomerId { get; set; }
        public string Customer { get; set; }
        public string Host { get; set; }
        public string SubDomain { get; set; }
        public string Logo { get; set; }
        public string ThemeColor { get; set; }
        public string ConnectionString { get; set; }
    }
}

