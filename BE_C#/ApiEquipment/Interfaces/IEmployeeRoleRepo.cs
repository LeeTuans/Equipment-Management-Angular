using ApiEquipment.Entities;

namespace ApiEquipment.Interfaces
{
    public interface IEmployeeRoleRepo : IRepo<EmployeeRole>
    {
        Task<EmployeeRole?> GetByEmployeeIdAndRoleIdAsync(int employeeId, int roleId);
        Task<ICollection<EmployeeRole>> GetByEmployeeIdAsync(int employeeId);
    }
}
