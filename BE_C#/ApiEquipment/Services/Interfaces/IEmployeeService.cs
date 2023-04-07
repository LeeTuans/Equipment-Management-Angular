using ApiEquipment.Dto;
using ApiEquipment.Entities;
using ApiEquipment.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEquipment.Services.Interfaces
{
    public interface IEmployeeService
    {

        Task<IEnumerable<DtoOutEmployee>> GetAll();

        Task<DtoOutEmployee?> GetById(int id);

        Task<bool?> Update(int id, DtoEmployee dtoEmployee);
        Task<DtoOutEmployee?> Add(DtoEmployee dtoEmployee);

        Task<bool?> Ban(int id);
        Task<bool?> UnBan(int id);

        Task<bool?> Delete(int id);
        Task<bool?> ChangePassword(int id, DtoChangePassword dtoChangePassword);
        Task<Employee?> LoginEmployee(string email, string password);

    }
}
