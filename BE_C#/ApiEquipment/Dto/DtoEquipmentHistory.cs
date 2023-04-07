
using ApiEquipment.Entities;
using System.ComponentModel.DataAnnotations;

namespace ApiEquipment.Dto
{
    public class DtoEquipmentHistory
    {
        [Required(ErrorMessage = "EmployeeId is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid EmployeeId")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "EquipmentId is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid EquipmentId")]
        public int EquipmentId { get; set; }
    }
}
