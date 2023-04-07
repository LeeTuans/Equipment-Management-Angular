using ApiEquipment.Dto;
using ApiEquipment.Helpers;

namespace ApiEquipment.Services.Interfaces
{

    public interface IEmployeeRoleService
    {

        Task<IEnumerable<DtoOutEmployeeRole>> GetAll();

        Task<DtoOutEmployeeRole?> GetById(int id);

        Task<bool?> Update(int id, DtoEmployeeRole dtoEmployeeRole);
        Task<DtoOutEmployeeRole?> Add(DtoEmployeeRole dtoEmployeeRole);
        Task<bool?> Delete(int id);
    }
}
