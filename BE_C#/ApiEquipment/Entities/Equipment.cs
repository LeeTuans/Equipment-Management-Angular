using ApiEquipment.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEquipment.Entities
{
    public class Equipment
    {
        [Key]
        public int EquipmentId { get; set; }

        
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
        [MaxLength(200)]
        [Required]
        public string Description { get; set; }
        [Required]
        public int EquipmentTypeId { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsAvailable { get; set; }

        public virtual EquipmentType EquipmentType { get; set; }

        public virtual ICollection<EquipmentHistory> EquipmentHistories { get; set; }
        public void SetValueFromDto(DtoEquipment dtoEquipment)
        {
            Name = dtoEquipment.Name;
            Description = dtoEquipment.Description;
            EquipmentTypeId = dtoEquipment.EquipmentTypeId;
            ImageUrl = dtoEquipment.ImageUrl;
            IsAvailable = dtoEquipment.IsAvailable;
        }
    }
}
