using System.ComponentModel.DataAnnotations;

namespace ApiEquipment.Dto
{
    public class DtoChangePassword
    {
        [Required(ErrorMessage = "Current password is required.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "New password is required.")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "The new password must be at least 4 characters.")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm new password is required.")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string NewPasswordConfirmed { get; set; }
    }
}