
using ApiEquipment.Entities;
using System.ComponentModel.DataAnnotations;

namespace ApiEquipment.Dto
{
    public class DtoEquipment
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name must not exceed 100 characters.")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Description must not exceed 500 characters.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "IsAvailable is required.")]
        public bool IsAvailable { get; set; }

        [Required(ErrorMessage = "Equipment type ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Equipment type ID must be a positive integer.")]
        public int EquipmentTypeId { get; set; }

        [Url(ErrorMessage = "Image URL is not a valid URL.")]
        public string? ImageUrl { get; set; }


    }
}
