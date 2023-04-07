using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ApiEquipment.Entities.Data
{
    public class EquipmentSeed : IEntityTypeConfiguration<Equipment>
    {
        public void Configure(EntityTypeBuilder<Equipment> builder)
        {
            builder.HasData(
                new Equipment
                {
                    EquipmentId = 1,
                    Name = "XAZCD",
                    Description = "PC mới nhất hãng Asus",
                    IsAvailable = true,
                    EquipmentTypeId = 1,
                },
                new Equipment
                {
                    EquipmentId = 2,
                    Name = "HPNEWXZ",
                    Description = "Laptop mới nhất hãng HP",
                    IsAvailable = false,
                    EquipmentTypeId = 2,

                },
                new Equipment
                {
                    EquipmentId = 3,
                    Name = "MSIQAX",
                    Description = "Laptop mới nhất hãng MSI",
                    IsAvailable = true,
                    EquipmentTypeId = 2,

                },
                new Equipment
                {
                    EquipmentId = 4,
                    Name = "MSIQAX",
                    Description = "PC mới nhất hãng MSI",
                    IsAvailable = true,
                    EquipmentTypeId = 1,

                },
                new Equipment
                {
                    EquipmentId = 5,
                    Name = "MSIQAX",
                    Description = "PC mới nhất hãng MSI",
                    IsAvailable = true,
                    EquipmentTypeId = 1,

                }
            );
        }
    }
}
