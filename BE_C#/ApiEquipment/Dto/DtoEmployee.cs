using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiEquipment.Dto
{
    public class DtoEmployee
    {
        [Required]
        [StringLength(50, ErrorMessage = "Name is too long.")]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Date, ErrorMessage = "Invalid date.")]
        public DateTime Birthdate { get; set; }

        [Url(ErrorMessage = "Invalid avatar URL.")]
        public string? AvatarUrl { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "At least one role is required.")]

        public ICollection<int> ListRoles { get; set; }
    }
}

