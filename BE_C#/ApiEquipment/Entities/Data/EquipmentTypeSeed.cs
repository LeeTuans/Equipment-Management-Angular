using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ApiEquipment.Entities.Data
{
    public class EquipmentTypeSeed : IEntityTypeConfiguration<EquipmentType>
    {
        public void Configure(EntityTypeBuilder<EquipmentType> builder)
        {
            builder.HasData(
                new EquipmentType
                {
                    EquipmentTypeId = 1,
                    TypeName = "PC",
                },
                new EquipmentType
                {
                    EquipmentTypeId = 2,
                    TypeName = "Laptop",
                }
            );
        }
    }
}
