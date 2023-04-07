using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ApiEquipment.Entities.Data
{
    public class EquipmentHistorySeed : IEntityTypeConfiguration<EquipmentHistory>
    {
        public void Configure(EntityTypeBuilder<EquipmentHistory> builder)
        {
            builder.HasData(
                new EquipmentHistory
                {
                    EquipmentHistoryId = 1,
                    EmployeeId = 1,
                    EquipmentId = 1,
                },
                new EquipmentHistory
                {
                    EquipmentHistoryId = 2,

                    EmployeeId = 3,
                    EquipmentId = 2,
                    IsAproved = true,
                }
            );
        }
    }
}
