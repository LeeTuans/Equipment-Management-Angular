using ApiEquipment.Entities;
using ApiEquipment.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiEquipment.Repositories
{
    public class EquipmentRepo : Repo<Equipment>, IEquipmentRepo
    {
        public EquipmentRepo(NewDBContext db) : base(db)
        {

        }
        public override async Task<Equipment?> GetById(int id)
        {
            return await _db.Set<Equipment>()
                .Include(e => e.EquipmentType)
                .Include(e => e.EquipmentHistories)
                .Where(e => e.EquipmentId == id)
                .FirstOrDefaultAsync();

        }

        public override async Task<IEnumerable<Equipment>> GetAll()
        {
            return await _db.Set<Equipment>()
                .Include(e => e.EquipmentType)
                .ToListAsync();
        }
    }
        
}
