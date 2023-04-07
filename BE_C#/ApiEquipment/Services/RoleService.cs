using ApiEquipment.Dto;
using ApiEquipment.Entities;
using ApiEquipment.Helpers;
using ApiEquipment.Interfaces;
using ApiEquipment.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiEquipment.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<DtoOutRole>> GetAll()
        {
            var roles = await _unitOfWork.GetRepo<IRoleRepo>().GetAll();
            
            List<DtoOutRole> DtoOutRoles = new List<DtoOutRole>();
            foreach (var r in roles)
            {
                var DtoOutRole = new DtoOutRole();
                DtoOutRole.SetValueFromEntity(r);
                DtoOutRoles.Add(DtoOutRole);
            }
            return DtoOutRoles;
        }
        public async Task<DtoOutRole?> GetById(int id)
        {
 
            var role = await _unitOfWork.GetRepo<IRoleRepo>().GetById(id);

            if (role == null)
            {
                return null;
            }
            var DtoOutRole = new DtoOutRole();
            DtoOutRole.SetValueFromEntity(role);
            return DtoOutRole;
        }

        public async Task<bool?> Update(int id, DtoRole dtoRole)
        {
           

            var emp = await _unitOfWork.GetRepo<IRoleRepo>().GetById(id);
            if (emp == null) return null;
            emp.SetValueFromDto(dtoRole);

            _unitOfWork.GetRepo<IRoleRepo>().Update(emp);

            var result = _unitOfWork.Save();

            if (result > 0)
                return true;
            else
                return false;
        }
        public async Task<DtoOutRole?> Add(DtoRole dtoRole)
        {
            var addRole = new Role();
            addRole.SetValueFromDto(dtoRole);
            await _unitOfWork.GetRepo<IRoleRepo>().Add(addRole);
            var result = _unitOfWork.Save();

            if (result <= 0) return null;

            var newRole = await _unitOfWork.GetRepo<IRoleRepo>().GetById(addRole.RoleId);

            if (newRole != null)
            {
                var DtoOutRole = new DtoOutRole();
                DtoOutRole.SetValueFromEntity(newRole);
                return DtoOutRole;
            }
            return null;
        }
        
        public async Task<bool?> Delete(int id)
        {
            
            var role = await _unitOfWork.GetRepo<IRoleRepo>().GetById(id);
            if (role == null)
            {
                return null;
            }
            _unitOfWork.GetRepo<IRoleRepo>().Delete(role);
            var result = _unitOfWork.Save();
            if (result <= 0) return false;
            return true;
        }

    }
}
