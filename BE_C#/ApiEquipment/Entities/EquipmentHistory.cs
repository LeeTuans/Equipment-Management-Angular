using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApiEquipment.Dto;

namespace ApiEquipment.Entities
{
    public class EquipmentHistory
    {
        [Key]
        public int EquipmentHistoryId { get; set; }
        [Required]
        public bool IsAproved { get; set; } = false;
        [Required]
        public DateTime BorrowedDate { get; set; } = DateTime.UtcNow;
        

        public DateTime? ReturnedDate { get; set; } = null;
        public int EmployeeId { get; set; }
        public int EquipmentId { get; set; }
        public virtual Employee Employee { get; set; }

        public virtual Equipment Equipment { get; set; }
        public void SetValueFromDto(DtoEquipmentHistory dtoEquipmentHistory)
        {
            EmployeeId = dtoEquipmentHistory.EmployeeId;
            EquipmentId = dtoEquipmentHistory.EquipmentId;
        }
    }

}
