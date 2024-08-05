using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace INFT3050.Models
{
    public class AuthUser : IdentityUser
    {
        [StringLength(100)]
        [MaxLength(100)]
        public string? Name { get; set; }
    }
}
