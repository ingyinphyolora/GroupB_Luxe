using System.ComponentModel.DataAnnotations;

namespace INFT3050.ViewModel
{
    public class ChangePasswordVM
    {
        public string? UserId { get; set; }

        public string? Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "The new password must be at least 8 characters long.")]
        public string? NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }
    }
}
