using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ApiEquipment.Helpers;
using ApiEquipment.GlobalClass;

namespace ApiEquipment.Entities.Data
{
    public class EmployeeSeed : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasData(
                new Employee
                {
                    EmployeeId = 1,
                    Name = "Hà Chánh Nam",
                    Email = "admin@gmail.com",
                    Password = MD5Hash.Hash("admin"),
                    Birthdate = new DateTime(2001, 10, 19)
                },
                new Employee
                {
                    EmployeeId = 2,
                    Name = "Ha Ha Ha",
                    Email = "hahaha@gmail.com",
                    Password = MD5Hash.Hash(EmployeeDefault.PASSWORD),
                    Birthdate = new DateTime(2001, 10, 19)
                },
                new Employee
                {
                    EmployeeId = 3,
                    Name = "Nam Nam Nam",
                    Email = "namnamnam@gmail.com",
                    Password = MD5Hash.Hash(EmployeeDefault.PASSWORD),
                    Birthdate = new DateTime(2001, 10, 19)

                },
                new Employee
                {
                    EmployeeId = 4,
                    Name = "Nam Chanh Ha",
                    Email = "namchanhha1@gmail.com",
                    Password = MD5Hash.Hash(EmployeeDefault.PASSWORD),
                    Birthdate = new DateTime(2001, 10, 19),
                }
            );
        }
    }
}
