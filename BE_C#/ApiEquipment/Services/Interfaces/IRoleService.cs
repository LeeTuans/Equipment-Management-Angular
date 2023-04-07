using ApiEquipment.Dto;
using ApiEquipment.Helpers;

namespace ApiEquipment.Services.Interfaces
{

    public interface IRoleService
    {

        Task<IEnumerable<DtoOutRole>> GetAll();

        Task<DtoOutRole?> GetById(int id);

        Task<bool?> Update(int id, DtoRole dtoRole);
        Task<DtoOutRole?> Add(DtoRole dtoRole);
        Task<bool?> Delete(int id);
    }
}
