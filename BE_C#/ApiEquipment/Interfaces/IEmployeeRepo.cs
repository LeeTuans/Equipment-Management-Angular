using ApiEquipment.Entities;

namespace ApiEquipment.Interfaces
{
    public interface IEmployeeRepo : IRepo<Employee>
    {
        Task<Employee?> GetByEmailAsync(string email);
        Task<Employee?> LoginEmployee(string email, string password);
        void BanEmployee(Employee employee);
        void UnBanEmployee(Employee employee);

        Task<List<int>> GetByBlacklistIdAsync();


    }
}
