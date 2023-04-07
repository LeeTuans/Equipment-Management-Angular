using ApiEquipment.Dto;
using ApiEquipment.Entities;
using ApiEquipment.Helpers;
using ApiEquipment.Interfaces;
using ApiEquipment.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiEquipment.Services
{
    public class EmployeeRoleService : IEmployeeRoleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeRoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<DtoOutEmployeeRole>> GetAll()
        {
            var employeeRoles = await _unitOfWork.GetRepo<IEmployeeRoleRepo>().GetAll();

            List<DtoOutEmployeeRole> dtoOutEmployeeRoles = new List<DtoOutEmployeeRole>();
            foreach (EmployeeRole employeeRole in employeeRoles)
            {
                var dtoOutEmployeeRole = new DtoOutEmployeeRole();
                dtoOutEmployeeRole.SetValueFromEntity(employeeRole);
                dtoOutEmployeeRoles.Add(dtoOutEmployeeRole);
            }
            return dtoOutEmployeeRoles;
        }


        public async Task<DtoOutEmployeeRole?> GetById(int id)
        {

            
            var employeeRole = await _unitOfWork.GetRepo<IEmployeeRoleRepo>().GetById(id);

            if (employeeRole == null)
            {
                return null;
            }
            var dtoOutEmployeeRole = new DtoOutEmployeeRole();
            dtoOutEmployeeRole.SetValueFromEntity(employeeRole);
            return dtoOutEmployeeRole;

        }


        public async Task<bool?> Update(int id, DtoEmployeeRole dtoEmployeeRole)
        {

            
            var empr = await _unitOfWork.GetRepo<IEmployeeRoleRepo>().GetById(id);
            if (empr == null) return null;
            var check = await _unitOfWork.GetRepo<IEmployeeRoleRepo>().GetByEmployeeIdAndRoleIdAsync(dtoEmployeeRole.EmployeeId, dtoEmployeeRole.RoleId);
            if (check != null) return false;
            
            empr.SetValueFromDto(dtoEmployeeRole);

            _unitOfWork.GetRepo<IEmployeeRoleRepo>().Update(empr);


            var result = _unitOfWork.Save();

            if (result > 0)
                return true;
            else
                return false;
        }

        public async Task<DtoOutEmployeeRole?> Add(DtoEmployeeRole dtoEmployeeRole)
        {

            
            var check = await _unitOfWork.GetRepo<IEmployeeRoleRepo>().GetByEmployeeIdAndRoleIdAsync(dtoEmployeeRole.EmployeeId, dtoEmployeeRole.RoleId);
            if (check != null)
            {
                return null;
            };

            var addEmployeeRole = new EmployeeRole();
            addEmployeeRole.SetValueFromDto(dtoEmployeeRole);
            await _unitOfWork.GetRepo<IEmployeeRoleRepo>().Add(addEmployeeRole);
            var result = _unitOfWork.Save();

            if (result <= 0) return null;


            var newEmployeeRole = await _unitOfWork.GetRepo<IEmployeeRoleRepo>().GetById(addEmployeeRole.EmployeeRoleId);

            if (newEmployeeRole != null)
            {
                var dtoOutEmployeeRole = new DtoOutEmployeeRole();

                dtoOutEmployeeRole.SetValueFromEntity(newEmployeeRole);
                return dtoOutEmployeeRole;
            }
            return null;
        }
        public async Task<bool?> Delete(int id)
        {

            var employeeRole = await _unitOfWork.GetRepo<IEmployeeRoleRepo>().GetById(id);
            if (employeeRole == null)
            {
                return null;
            }

            _unitOfWork.GetRepo<IEmployeeRoleRepo>().Delete(employeeRole);
            var result = _unitOfWork.Save();
            if(result <= 0) return false;
            return true;
        }
    }
}
