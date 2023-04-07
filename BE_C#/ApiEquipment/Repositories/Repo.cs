using Microsoft.EntityFrameworkCore;
using ApiEquipment.Interfaces;
using ApiEquipment.Entities;

namespace ApiEquipment.Repositories
{
    public class Repo<T> : IRepo<T> where T : class
    {
        protected readonly NewDBContext _db;
        public Repo(NewDBContext db)
        {
            this._db = db;
        }
        public virtual async Task<T?> GetById(int id)
        {
            return await _db.Set<T>().FindAsync(id);
             
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await _db.Set<T>().ToListAsync();
        }

        public virtual async Task Add(T entity)
        {
            await _db.Set<T>().AddAsync(entity);
        }

        public virtual void Delete(T entity)
        {
            _db.Set<T>().Remove(entity);
        }

        public virtual void Update(T entity)
        {
            _db.Set<T>().Update(entity);
        }

    }
}
