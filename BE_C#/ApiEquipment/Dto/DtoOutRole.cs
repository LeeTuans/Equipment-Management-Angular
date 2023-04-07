using ApiEquipment.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;

namespace ApiEquipment.Dto
{
    public class DtoOutRole
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public void SetValueFromEntity(Role role)
        {
            RoleId = role.RoleId;
            RoleName = role.RoleName;
        }
    }
}
