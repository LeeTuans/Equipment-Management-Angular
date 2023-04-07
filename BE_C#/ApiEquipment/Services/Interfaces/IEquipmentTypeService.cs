using ApiEquipment.Dto;
using ApiEquipment.Helpers;

namespace ApiEquipment.Services.Interfaces
{
    public interface IEquipmentTypeService
    {


        Task<IEnumerable<DtoOutEquipmentType>> GetAll();

        Task<DtoOutEquipmentType?> GetById(int id);

        Task<bool?> Update(int id, DtoEquipmentType dtoEquipmentType);
        Task<DtoOutEquipmentType?> Add(DtoEquipmentType dtoEquipmentType);
        Task<bool?> Delete(int id);
    }
}
