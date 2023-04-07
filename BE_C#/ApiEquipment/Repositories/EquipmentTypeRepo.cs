using ApiEquipment.Entities;
using ApiEquipment.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiEquipment.Repositories
{
    
    public class EquipmentTypeRepo : Repo<EquipmentType>, IEquipmentTypeRepo
    {
        public EquipmentTypeRepo(NewDBContext db) : base(db)
        {

        }

    }
}
