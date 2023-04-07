using ApiEquipment.Entities;
using ApiEquipment.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiEquipment.Repositories
{
   
    public class EmployeeRoleRepo : Repo<EmployeeRole>, IEmployeeRoleRepo
    {
        public EmployeeRoleRepo(NewDBContext db) : base(db)
        {
        }
        public async Task<EmployeeRole?> GetByEmployeeIdAndRoleIdAsync(int employeeId, int roleId)
        {
            return await _db.Set<EmployeeRole>().Where(er => er.RoleId == roleId && er.EmployeeId == employeeId).FirstOrDefaultAsync();
        }
        public async Task<ICollection<EmployeeRole>> GetByEmployeeIdAsync(int employeeId)
        {
            return await _db.Set<EmployeeRole>().Where(er =>er.EmployeeId == employeeId).ToListAsync();
        }
    }
}
