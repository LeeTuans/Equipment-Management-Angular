using ApiEquipment.Entities;
using ApiEquipment.GlobalClass;
using ApiEquipment.Helpers;
using ApiEquipment.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiEquipment.Repositories
{
    public class EmployeeRepo : Repo<Employee>, IEmployeeRepo
    {
        public EmployeeRepo(NewDBContext db) : base(db)
        {
        }

        public override async Task<Employee?> GetById(int id)
        {
            return await _db.Set<Employee>()
                .Include(e => e.EmployeeRoles)
                .ThenInclude(er => er.Role)
                .Where(e => e.EmployeeId == id)
                .FirstOrDefaultAsync();

        }

        public override async Task<IEnumerable<Employee>> GetAll()
        {
            return await _db.Set<Employee>()
                            .Include(e => e.EmployeeRoles)
                            .ThenInclude(er => er.Role)
                            .ToListAsync();
        }
        public async Task<Employee?> GetByEmailAsync(string email)
        {
            return await _db.Set<Employee>().Where(e => e.Email == email).FirstOrDefaultAsync();
        }
        
        public async Task<Employee?> LoginEmployee(string email, string password)
        {
            var employeePassword = MD5Hash.Hash(password);

            return await _db.Employees
                .Include(e => e.EmployeeRoles).ThenInclude(er => er.Role).Where(u => u.Email == email && u.Password == employeePassword).FirstOrDefaultAsync();
        }
        public void BanEmployee(Employee employee)
        {
            employee.IsBan = true;
            _db.Set<Employee>().Update(employee);
        }
        public void UnBanEmployee(Employee employee)
        {
            employee.IsBan = false;
            _db.Set<Employee>().Update(employee);
        }
        public async Task<List<int>> GetByBlacklistIdAsync()
        {
            return await _db.Set<Employee>().Where(e => e.IsBan == true).Select(e => e.EmployeeId).ToListAsync();
        }
    }
}
