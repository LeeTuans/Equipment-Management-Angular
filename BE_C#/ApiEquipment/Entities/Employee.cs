using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApiEquipment.Dto;
using ApiEquipment.GlobalClass;
using ApiEquipment.Helpers;

namespace ApiEquipment.Entities
{
    public class Employee
    {

        [Key]
        public int EmployeeId { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        [MaxLength(50)]
        [Required]
        public string Email { get; set; }

        [MaxLength(50)]
        [Required]
        public string Password { get; set; } = MD5Hash.Hash(EmployeeDefault.PASSWORD); //32char

        [Required]
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }
        [MaxLength(250)]
        public string? AvatarUrl { get; set; }
        [Required]
        public bool IsBan { get; set; } = false;
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }

        public virtual ICollection<EmployeeRole> EmployeeRoles { get; set; }
        public virtual ICollection<EquipmentHistory> EquipmentHistories { get; set; }

        public void SetValueFromDto(DtoEmployee dtoEmployee)
        {
            Name = dtoEmployee.Name;
            Email = dtoEmployee.Email;
            Birthdate = dtoEmployee.Birthdate;
            AvatarUrl = dtoEmployee.AvatarUrl;
        }
    }
    public class Jwt
    {
        public string key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int TokenValidityInMinutes { get; set; }
        public int RefreshTokenValidityInDays { get; set; }

        
    }
}

