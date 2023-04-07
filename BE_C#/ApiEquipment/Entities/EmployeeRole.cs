using ApiEquipment.Dto;
using System.ComponentModel.DataAnnotations;

namespace ApiEquipment.Entities
{
    public class EmployeeRole
    {
        [Key]
        public int EmployeeRoleId { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public int RoleId { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Role Role { get; set; }
        public void SetValueFromDto(DtoEmployeeRole dtoEmployeeRole)
        {
            EmployeeId = dtoEmployeeRole.EmployeeId;
            RoleId = dtoEmployeeRole.RoleId;
        }
    }
}
