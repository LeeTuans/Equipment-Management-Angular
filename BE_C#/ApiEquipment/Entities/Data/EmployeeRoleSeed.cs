using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ApiEquipment.Entities.Data
{
    public class EmployeeRoleSeed : IEntityTypeConfiguration<EmployeeRole>
    {
        public void Configure(EntityTypeBuilder<EmployeeRole> builder)
        {
            builder.HasData(
                new EmployeeRole
                {
                    EmployeeRoleId = 1,
                    EmployeeId = 1,
                    RoleId = 1,
                },
                new EmployeeRole
                {
                    EmployeeRoleId = 2,
                    EmployeeId = 1,
                    RoleId = 2,
                },
                new EmployeeRole
                {
                    EmployeeRoleId = 3,
                    EmployeeId = 2,
                    RoleId = 1,
                },
                new EmployeeRole
                {
                    EmployeeRoleId = 4,
                    EmployeeId = 3,
                    RoleId = 1,
                },
                new EmployeeRole
                {
                    EmployeeRoleId = 5,
                    EmployeeId = 4,
                    RoleId = 2,
                }
            );
        }
    }
}
