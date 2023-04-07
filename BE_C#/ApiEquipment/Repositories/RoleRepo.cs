using ApiEquipment.Entities;
using ApiEquipment.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiEquipment.Repositories
{
    public class RoleRepo : Repo<Role>, IRoleRepo
    {
        public RoleRepo(NewDBContext db) : base(db)
        {

        }
    }
}
