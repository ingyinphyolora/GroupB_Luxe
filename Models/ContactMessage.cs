using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace INFT3050.Models
{
    public class ContactMessage 
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Message { get; set; }

        public DateTime SubmittedAt { get; set; } = DateTime.Now;
    }
}
