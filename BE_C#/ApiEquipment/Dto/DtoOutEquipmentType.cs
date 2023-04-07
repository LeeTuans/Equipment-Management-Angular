
using ApiEquipment.Entities;
using System.Data;

namespace ApiEquipment.Dto
{
    public class DtoOutEquipmentType
    {
        public int EquipmentTypeId { get; set; }
        public string TypeName { get; set; }
        public void SetValueFromEntity(EquipmentType equipmentType)
        {
                EquipmentTypeId = equipmentType.EquipmentTypeId;
                TypeName = equipmentType.TypeName;
        }
    }
}
