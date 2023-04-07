using ApiEquipment.Dto;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace ApiEquipment.Entities
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        [Required]
        [MaxLength(50)]

        public string RoleName { get; set; }
        public virtual ICollection<EmployeeRole> EmployeeRoles { get; set; }
        public void SetValueFromDto(DtoRole dtoRole)
        {
            RoleName = dtoRole.RoleName;
        }
    }
}
