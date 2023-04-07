namespace ApiEquipment.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        //IEmployeeRepo Employees { get; }

        //IEmployeeRoleRepo EmployeeRoles { get; }

        //IEquipmentHistoryRepo EquipmentHistories { get; }
        //IEquipmentRepo Equipments { get; }
        //IEquipmentTypeRepo EquipmentTypes { get; }
        //IRoleRepo Roles { get; }
        IEntityRepo GetRepo<IEntityRepo>() where IEntityRepo : class;
        int Save();
    }


}
