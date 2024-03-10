using MessagePack;
using System.ComponentModel.DataAnnotations;

namespace MultiTenantApp.Models
{
    public class Users
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string UserEmail { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
