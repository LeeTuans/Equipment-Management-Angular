using ApiEquipment.Entities;

namespace ApiEquipment.Interfaces
{
    public interface IEquipmentHistoryRepo : IRepo<EquipmentHistory>
    {
        Task<IEnumerable<EquipmentHistory>> GetByEmployeeId(int id);
        Task<IEnumerable<EquipmentHistory>> GetEquipmentAssignedByEmployeeId(int id);

    }
}
