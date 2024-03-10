using System.ComponentModel.DataAnnotations;

namespace MultiTenantApp
{
    public class TenantUsers
    {
        [Key]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Email { get; set; }
    }
}
