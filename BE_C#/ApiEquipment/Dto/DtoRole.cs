using ApiEquipment.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace ApiEquipment.Dto
{
    public class DtoRole
    {
        [Required(ErrorMessage = "Role name is required.")]
        [StringLength(100, ErrorMessage = "RoleType name must not exceed 100 characters.")]
        public string RoleName { get; set; }
    }
}
