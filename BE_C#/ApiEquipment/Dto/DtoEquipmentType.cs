using System.ComponentModel.DataAnnotations;

namespace ApiEquipment.Dto
{
    public class DtoEquipmentType
    {
        [Required(ErrorMessage = "Type name is required.")]
        [StringLength(100, ErrorMessage = "Type name must not exceed 100 characters.")]
        public string TypeName { get; set; }
    }
}