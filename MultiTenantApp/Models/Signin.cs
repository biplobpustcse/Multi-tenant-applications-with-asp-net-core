using System.ComponentModel.DataAnnotations;

namespace MultiTenantApp.Models
{
    public class Signin
    {
        [Required(ErrorMessage ="email address is required")]
        [EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
