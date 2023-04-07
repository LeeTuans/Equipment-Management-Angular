using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ApiEquipment.Dto
{
    public class DtoLogin
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Email is not a valid email address.")]
        [DefaultValue("admin@gmail.com")]

        public string Email { get; set; } 

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Password must be at least 4 characters long.")]
        [DefaultValue("admin")]
        public string Password { get; set; }

    }
}