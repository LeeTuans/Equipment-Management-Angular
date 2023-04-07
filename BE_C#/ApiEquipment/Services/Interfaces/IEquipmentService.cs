using ApiEquipment.Dto;
using ApiEquipment.Entities;
using ApiEquipment.Helpers;

namespace ApiEquipment.Services.Interfaces
{
    public interface IEquipmentService
    {
        Task<IEnumerable<DtoOutEquipment>> GetAll();

        Task<DtoOutEquipment?> GetById(int id);

        Task<bool?> Update(int id, DtoEquipment dtoEquipment);
        Task<DtoOutEquipment?> Add(DtoEquipment dtoEquipment);
        Task<bool?> Delete(int id);
    }
}

