using ApiEquipment.Entities;
using ApiEquipment.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ApiEquipment.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly NewDBContext _dbcontext;
        private Dictionary<Type, object> _repositories;

        //public IEmployeeRepo Employees { get; }
        //public IEmployeeRoleRepo EmployeeRoles { get; }
        //public IEquipmentHistoryRepo EquipmentHistories { get; }
        //public IEquipmentRepo Equipments { get; }
        //public IEquipmentTypeRepo EquipmentTypes { get; }
        //public IRoleRepo Roles { get; }


        public UnitOfWork(NewDBContext dbContext
            //,
            //IEmployeeRepo employeeRepo,
            //IEmployeeRoleRepo employeeRoleRepo,
            //IEquipmentHistoryRepo equipmentHistoryRepo,
            //IEquipmentRepo equipmentRepo,
            //IEquipmentTypeRepo equipmentTypeRepo,
            //IRoleRepo roleRepo
           )
        {
            this._dbcontext = dbContext;

            //this.Employees = employeeRepo;
            //this.EmployeeRoles = employeeRoleRepo;
            //this.EquipmentHistories = equipmentHistoryRepo;
            //this.Equipments = equipmentRepo;
            //this.EquipmentTypes = equipmentTypeRepo;
            //this.Roles = roleRepo;
            if (_repositories == null)
            {
                _repositories = new Dictionary<Type, object>();
            }
        }
        //public IRepo<T> GetRepo<T>(bool CustomRepo = false) where T : class
        //{    
        //    var typeEntity = typeof(T);
        //    if (!_repositories.ContainsKey(typeEntity))
        //    {
        //        _repositories[typeEntity] = new Repo<T>(_dbcontext);
        //    }
        //    return (IRepo<T>)_repositories[typeEntity];
        //}

        public IEntityRepo GetRepo<IEntityRepo>() where IEntityRepo : class
        {
            //var customRepo = _dbcontext.GetService<IEntityRepo>();
            //return customRepo;
            var type = typeof(IEntityRepo);
            if (!_repositories.ContainsKey(type))
            {
                _repositories[type] = _dbcontext.GetService<IEntityRepo>();
            }
            return (IEntityRepo)_repositories[type];

        }
        public IRepo<Entity> GetGenericRepo<Entity>() where Entity : class
        {
            var type = typeof(Entity);
            if (!_repositories.ContainsKey(type))
            {
                _repositories[type] = new Repo<Entity>(_dbcontext);
            }
            return (IRepo<Entity>)_repositories[type];
        }
        public int Save()
        {
            return _dbcontext.SaveChanges();
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbcontext.Dispose();
            }
        }

        
    }
}
