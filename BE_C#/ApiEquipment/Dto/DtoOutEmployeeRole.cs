
using ApiEquipment.Entities;

namespace ApiEquipment.Dto
{
    public class DtoOutEmployeeRole
    {

        public int EmployeeRoleId { get; set; }

        public int EmployeeId { get; set; }
        public int RoleId { get; set; }
        public void SetValueFromEntity(EmployeeRole employeeRole)
        {
            EmployeeRoleId = employeeRole.EmployeeRoleId;
            EmployeeId = employeeRole.EmployeeId;
            RoleId = employeeRole.RoleId;
        }
    }
}
