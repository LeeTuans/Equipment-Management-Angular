using ApiEquipment.Dto;
using System.ComponentModel.DataAnnotations;

namespace ApiEquipment.Entities
{
    public class EquipmentType
    {
        [Key]
        public int EquipmentTypeId { get; set; }
        [Required]
        [MaxLength(50)]
        public string TypeName { get;set; }
        public void SetValueFromDto(DtoEquipmentType dtoEquipmentType)
        {
            TypeName = dtoEquipmentType.TypeName;
        }
    }
}
