using ApiEquipment.Dto;
using ApiEquipment.Entities;
using ApiEquipment.Helpers;

namespace ApiEquipment.Services.Interfaces
{
    public interface IEquipmentHistoryService
    {
        Task<IEnumerable<DtoOutEquipmentHistory>> GetAll();

        Task<DtoOutEquipmentHistory?> GetById(int id);
        Task<IEnumerable<DtoOutEquipmentHistory>> GetByEmployeeId(int id);
        Task<IEnumerable<DtoOutEquipmentHistory>> GetEquipmentAssignedByEmployeeId(int id);


        Task<bool?> Update(int id, DtoEquipmentHistory dtoEquipmentHistory);
        Task<DtoOutEquipmentHistory?> Add(DtoEquipmentHistory dtoEquipmentHistory);
        Task<bool?> Delete(int id);
        Task<bool?> Approve(int id);
        Task<bool?> CheckReturn(int id);
    }
}
