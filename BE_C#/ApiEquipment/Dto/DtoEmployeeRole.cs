
using ApiEquipment.Entities;
using System.ComponentModel.DataAnnotations;

namespace ApiEquipment.Dto
{
    public class DtoEmployeeRole
    {
        [Required(ErrorMessage = "Employee ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Employee ID must be a positive integer.")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Role ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Role ID must be a positive integer.")]
        public int RoleId { get; set; }
    }
}
