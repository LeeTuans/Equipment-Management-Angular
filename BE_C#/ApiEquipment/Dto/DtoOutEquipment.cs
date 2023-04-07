
using ApiEquipment.Entities;

namespace ApiEquipment.Dto
{
    public class DtoOutEquipment
    {
        public int EquipmentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsAvailable { get; set; }
        public string? ImageUrl { get; set; }
        public DtoOutEquipmentType? EquipmentType { get; set; }
        public void SetValueFromEntity(Equipment equipment)
        {
            EquipmentId = equipment.EquipmentId;
            Name = equipment.Name;
            Description = equipment.Description;
            //EquipmentTypeId = equipment.EquipmentTypeId;
            ImageUrl = equipment.ImageUrl;
            IsAvailable = equipment.IsAvailable;
            EquipmentType = new DtoOutEquipmentType();
            EquipmentType.SetValueFromEntity(equipment.EquipmentType);
        }
    }
}
