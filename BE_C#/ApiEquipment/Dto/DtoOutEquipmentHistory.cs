
using ApiEquipment.Entities;

namespace ApiEquipment.Dto
{
    public class DtoOutEquipmentHistory
    {
        public int EquipmentHistoryId { get; set; }
        public bool IsAproved { get; set; } = false;

        public DateTime BorrowedDate { get; set; }
        public DateTime? ReturnedDate { get; set; }

        public DtoOutEmployee? Employee { get; set; }
        public DtoOutEquipment? Equipment { get; set; }

        public void SetValueFromEntity(EquipmentHistory equipmentHistory)
        {
            EquipmentHistoryId = equipmentHistory.EquipmentHistoryId;
            IsAproved = equipmentHistory.IsAproved;
            BorrowedDate = equipmentHistory.BorrowedDate;
            ReturnedDate = equipmentHistory.ReturnedDate;
            if(equipmentHistory.Employee != null)
            {
                Employee = new DtoOutEmployee();
                Employee.SetValueFromEntity(equipmentHistory.Employee);
            }
            if(equipmentHistory.Equipment != null)
            {
                Equipment = new DtoOutEquipment();
                Equipment.SetValueFromEntity(equipmentHistory.Equipment);
            }
        }
    }
}
