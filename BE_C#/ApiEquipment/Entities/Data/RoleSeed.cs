using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ApiEquipment.Entities.DataSeed
{
    public class RoleSeed : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(
                new Role
                {
                    RoleId = 1,
                    RoleName = "User",
                },
                new Role
                {
                    RoleId = 2,
                    RoleName = "Admin",
                }
            );
        }
    }
}
