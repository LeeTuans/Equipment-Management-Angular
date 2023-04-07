using ApiEquipment.Entities;
using ApiEquipment.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiEquipment.Repositories
{
    public class EquipmentHistoryRepo : Repo<EquipmentHistory>, IEquipmentHistoryRepo
    {
        public EquipmentHistoryRepo(NewDBContext db) : base(db)
        { }
        public override async Task<EquipmentHistory?> GetById(int id)
        {
            return await _db.Set<EquipmentHistory>()
                .Include(eh => eh.Employee)
                .Include(eh => eh.Equipment)
                .ThenInclude(ehe => ehe.EquipmentType)
                .Where(eh => eh.EquipmentHistoryId == id)
                .FirstOrDefaultAsync();

        }
        
        public async Task<IEnumerable<EquipmentHistory>> GetByEmployeeId(int id)
        {
            return await _db.Set<EquipmentHistory>()
                .Where(eh => eh.EmployeeId == id)
                .Include(eh => eh.Equipment)
                .ThenInclude(ehe => ehe.EquipmentType)
                .ToListAsync();
        }
        public async Task<IEnumerable<EquipmentHistory>> GetEquipmentAssignedByEmployeeId(int id)
        {
            return await _db.Set<EquipmentHistory>()
                .Include(eh => eh.Equipment)
                .ThenInclude(ehe => ehe.EquipmentType)
                .Where(eh => eh.EmployeeId == id && eh.IsAproved == true && eh.ReturnedDate == null)
                .ToListAsync();
        }
        
        public override async Task<IEnumerable<EquipmentHistory>> GetAll()
        {
            return await _db.Set<EquipmentHistory>()
                .Include(eh => eh.Employee)
                .Include(eh => eh.Equipment)
                .ThenInclude(ehe => ehe.EquipmentType)
                .ToListAsync();
        }
    }
    
}
