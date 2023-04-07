using ApiEquipment.Dto;
using ApiEquipment.Entities;
using ApiEquipment.Helpers;
using ApiEquipment.Interfaces;
using ApiEquipment.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;

using System.Data;

namespace ApiEquipment.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        
        public async Task<IEnumerable<DtoOutEmployee>> GetAll()

        {
            var employees = await _unitOfWork.GetRepo<IEmployeeRepo>().GetAll();
            
            List<DtoOutEmployee> dtoOutEmployees = new List<DtoOutEmployee>();

            foreach (Employee e in employees)
            {
                var dtoOutEmployee = new DtoOutEmployee();
                dtoOutEmployee.SetValueFromEntity(e);
                dtoOutEmployees.Add(dtoOutEmployee);
            }
            return  dtoOutEmployees;
        }


        public async Task<DtoOutEmployee?> GetById(int id)
        {
            var employee = await _unitOfWork.GetRepo<IEmployeeRepo>().GetById(id);

            if (employee == null)
            {
                return null;
            }
            var dtoOutEmployee = new DtoOutEmployee();
            dtoOutEmployee.SetValueFromEntity(employee);
            return dtoOutEmployee;
        }
        public async Task<bool?> Update(int id, DtoEmployee dtoEmployee)

        {
            var emp = await _unitOfWork.GetRepo<IEmployeeRepo>().GetById(id);

            if (emp == null) return null;
            
            if (!emp.Email.Equals(dtoEmployee.Email))
            {
                var check = await _unitOfWork.GetRepo<IEmployeeRepo>().GetByEmailAsync(dtoEmployee.Email);
                if (check != null)
                {
                    return false;
                }
            }
            emp.SetValueFromDto(dtoEmployee);
            _unitOfWork.GetRepo<IEmployeeRepo>().Update(emp);

            var employeeRoles = await _unitOfWork.GetRepo<IEmployeeRoleRepo>().GetByEmployeeIdAsync(emp.EmployeeId);

            foreach(var er in employeeRoles)
            {
                _unitOfWork.GetRepo<IEmployeeRoleRepo>().Delete(er);
            }
            foreach (var roleId in dtoEmployee.ListRoles)
            {
                var addEmployeeRole = new EmployeeRole()
                {
                    EmployeeId = emp.EmployeeId,
                    RoleId = roleId,
                };
                await _unitOfWork.GetRepo<IEmployeeRoleRepo>().Add(addEmployeeRole);
            }
            var result = _unitOfWork.Save();

            if (result > 0)
                return true;
            else
                return false;
        }


        public async Task<DtoOutEmployee?> Add(DtoEmployee dtoEmployee)
        {

            var check = await _unitOfWork.GetRepo<IEmployeeRepo>().GetByEmailAsync(dtoEmployee.Email);
            if (check != null)
            {
                return null;
            }
            var addEmployee = new Employee();
            addEmployee.SetValueFromDto(dtoEmployee);
            addEmployee.EmployeeRoles = new List<EmployeeRole>();

            if (dtoEmployee.ListRoles.Count() > 0)
            {
                foreach (var roleId in dtoEmployee.ListRoles.Distinct())
                {
                    var checkRole = await _unitOfWork.GetRepo<IRoleRepo>().GetById(roleId);
                    if (checkRole != null)
                    {
                        var addEmployeeRole = new EmployeeRole()
                        {
                            RoleId = roleId,
                        };
                        addEmployee.EmployeeRoles.Add(addEmployeeRole);
                    }
                }
            }
            await _unitOfWork.GetRepo<IEmployeeRepo>().Add(addEmployee);

            var result = _unitOfWork.Save();

            if (result <= 0) return null;

            var newEmployee = await _unitOfWork.GetRepo<IEmployeeRepo>().GetById(addEmployee.EmployeeId);

            if(newEmployee != null)
            {
                var dtoOutEmployee = new DtoOutEmployee();

                dtoOutEmployee.SetValueFromEntity(newEmployee);
                return dtoOutEmployee;

            }
            return null;
        }
        public async Task<bool?> Delete(int id)
        {
            
            var employee = await _unitOfWork.GetRepo<IEmployeeRepo>().GetById(id);
            
            if (employee == null)
            {
                return null;
            }
            if (employee.EmployeeRoles.Where(er => er.Role.RoleName == GlobalClass.ClaimRoles.ADMIN).FirstOrDefault() != null) {
                return false;

            }
            _unitOfWork.GetRepo<IEmployeeRepo>().Delete(employee);
            var result = _unitOfWork.Save();
            if (result <= 0) return false;
            return true;

        }
        [HttpPut("ChangePassword/{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<bool?> ChangePassword(int id, DtoChangePassword dtoChangePassword)

        {
            var emp = await _unitOfWork.GetRepo<IEmployeeRepo>().GetById(id);

            if (emp == null) return null;

            var encodePassword = MD5Hash.Hash(dtoChangePassword.Password);

            if (emp.Password == encodePassword && dtoChangePassword.NewPassword == dtoChangePassword.NewPasswordConfirmed)
            {
                var encodeNewPassword = MD5Hash.Hash(dtoChangePassword.NewPassword);
                emp.Password = encodeNewPassword;
                _unitOfWork.GetRepo<IEmployeeRepo>().Update(emp);
                var result = _unitOfWork.Save();
                return true;
            };
            return false;
        }
        public Task<Employee?> LoginEmployee(string email, string password)
        {
            IEmployeeRepo employeeRepo = _unitOfWork.GetRepo<IEmployeeRepo>();

            return employeeRepo.LoginEmployee(email, password);
        }
        public async Task<bool?> Ban(int id)
        {

            var employee = await _unitOfWork.GetRepo<IEmployeeRepo>().GetById(id);
            if (employee == null)
            {
                return null;
            }
            if(employee.EmployeeRoles.Where(er => er.Role.RoleName == GlobalClass.ClaimRoles.ADMIN) != null) {
                return false;
            }
            _unitOfWork.GetRepo<IEmployeeRepo>().BanEmployee(employee);
            var result = _unitOfWork.Save();
            if (result <= 0) return false;
            return true;

        }
        public async Task<bool?> UnBan(int id)
        {

            var employee = await _unitOfWork.GetRepo<IEmployeeRepo>().GetById(id);
            if (employee == null)
            {
                return null;
            }
            _unitOfWork.GetRepo<IEmployeeRepo>().UnBanEmployee(employee);
            var result = _unitOfWork.Save();
            if (result <= 0) return false;
            return true;
        }
    }

}


