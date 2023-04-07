using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiEquipment.Entities;
using ApiEquipment.Dto;
using ApiEquipment.Interfaces;
using ApiEquipment.Helpers;
using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using ApiEquipment.Services.Interfaces;
using ApiEquipment.Services;
using ApiEquipment.GlobalClass;
using Microsoft.Extensions.Primitives;

namespace ApiEquipment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // GET: api/Employees
        [HttpGet]
        [Authorize(Roles = ClaimRoles.ADMIN)]

        public async Task<ActionResult<Respone>> GetEmployees()
        {
            
                var result = await _employeeService.GetAll();
                return Ok(new Respone()
                {
                    Status = true,
                    Message = "Success!",
                    Data= result,
                });
            
        }

        //GET: api/Employees/5
        [HttpGet("{id}")]
        //[AllowAnonymous]
        [Authorize]

        public async Task<ActionResult<Respone>> GetEmployee(int id)
        {
            if (!User.IsInRole(ClaimRoles.ADMIN))
            {
                var idClaim = User.Claims.FirstOrDefault(c => c.Type == "Id");
                if (idClaim != null)
                {
                   var value = int.Parse(idClaim.Value);
                    if (value != id)
                    {
                        return StatusCode(StatusCodes.Status403Forbidden, new Respone()
                        {
                            Status = false,
                            Message = "You do not have permission to access this resource.",
                        }); ;
                    }
                }
            }
            
            if (id <= 0) return BadRequest(new Respone()
            {
                Message = "Bad request!",
            });
            try
            {
                var result = await _employeeService.GetById(id);
                if (result == null)
                {
                    return NotFound(new Respone()
                    {
                        Status = false,
                        Message = "Not found!",
                    });
                }
                return Ok(new Respone()
                {
                    Status = true,
                    Message = "Success!",
                    Data = result,
                });
            }
           
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new Respone()
                {
                    Status = false,
                    Message = "An error occurred while processing your request.",
                });
            }
        }
        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]

        public async Task<ActionResult<Respone>> PutEmployee(int id, [FromBody] DtoEmployee dtoEmployee)
        {
            if (User.IsInRole(ClaimRoles.ADMIN))
            {
                var idClaim = User.Claims.FirstOrDefault(c => c.Type == "Id");
                if (idClaim != null)
                {
                    var value = int.Parse(idClaim.Value);
                    var emp = await _employeeService.GetById(id);
                    if (emp!=null && emp.Roles != null)
                    {
                        if (value != id && emp.Roles.Where(er => er.RoleName == ClaimRoles.ADMIN).FirstOrDefault() != null)
                        {
                            return StatusCode(StatusCodes.Status403Forbidden, new Respone()
                            {
                                Status = false,
                                Message = "You do not have permission to access this resource.",
                            }); ;
                        }
                    }
                    
                }
            }
            if (User.IsInRole(ClaimRoles.USER))
            {
                var idClaim = User.Claims.FirstOrDefault(c => c.Type == "Id");
                if (idClaim != null)
                {
                    var value = int.Parse(idClaim.Value);
                    if (value != id)
                    {
                        return StatusCode(StatusCodes.Status403Forbidden, new Respone()
                        {
                            Status = false,
                            Message = "You do not have permission to access this resource.",
                        }); ;
                    }
                }
            }
            if (id <= 0) return BadRequest(new Respone()
            {
                Status = false,
                Message = "Bad request!",
            });
            var result = await _employeeService.Update(id, dtoEmployee);
            if (result == null) return NotFound(new Respone()
            {
                Status = false,
                Message = "Not found!"
            });

            else if (result == false)
            {
                return BadRequest(new Respone()
                {
                    Status = false,
                    Message = "Loi du lieu!",
                });
            }
            else return Ok(new Respone()
            {
                Status = true,
                Message = "Success!",
            });


        }

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = ClaimRoles.ADMIN)]

        public async Task<ActionResult<Respone>> PostEmployee([FromBody] DtoEmployee dtoEmployee)
        {
            if (dtoEmployee == null)
            {
                return BadRequest(new Respone()
                {
                    Status = false,
                    Message = "Bad request!",
                });
            }

            var result = await _employeeService.Add(dtoEmployee);
            if (result == null) return BadRequest(new Respone()
            {
                Status = false,
                Message = "Bad request!",
            });
            else
                return Ok(new Respone()
                {
                    Status = true,
                    Message = "Success!",
                    Data = result,
                });

        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        [Authorize(Roles = ClaimRoles.ADMIN)]

        public async Task<ActionResult<Respone>> DeleteEmployee(int id)
        {
            var idClaim = User.Claims.FirstOrDefault(c => c.Type == "Id");
            if (idClaim != null)
            {
                var value = int.Parse(idClaim.Value);
                if (value == id)
                {
                    return StatusCode(StatusCodes.Status403Forbidden, new Respone()
                    {
                        Status = false,
                        Message = "You can't delete yoursefl.",
                        Data = null,
                    }); ;
                }
            }
            var deleteEmployee = await _employeeService.GetById(id);
            




            if (id <= 0) return BadRequest(new Respone()
            {
                Status = false,
                Message = "Bad request!",
            });
            var result = await _employeeService.Delete(id);

            if (result == null) return NotFound(new Respone()
            {
                Status = false,
                Message = "Not found!"
            });
            else if (result == false) return BadRequest(new Respone()
            {
                Status = false,
                Message = "Bad request!",
            });
            else return Ok(new Respone()
            {
                Status = true,
                Message = "Success!",
            });
        }
        [HttpPut("ChangePassword/{id}")]
        [Authorize]

        public async Task<ActionResult<Respone>> ChangePassword(int id, [FromBody] DtoChangePassword dtoChangePassword)
        {
            if (id <= 0) return BadRequest(new Respone()
            {Status = false,
                Message = "Bad request!",
            });
            var result = await _employeeService.ChangePassword(id, dtoChangePassword);
            if (result == null) return NotFound(new Respone()
            {
                Status = false,
                Message = "Not found!"
            });
            if (result == false) return BadRequest(new Respone()
            {
                Status = false,
                Message = "Wrong password!",
            });
            return Ok(new Respone()
            {
                Status = true,
                Message = "Success!",
            });
        }
        [HttpPut("Ban/{id}")]
        [Authorize(Roles = ClaimRoles.ADMIN)]

        public async Task<ActionResult<Respone>> BanEmployee(int id,[FromBody] DtoEmployee dtoEmployee)
        {
            if (User.IsInRole(ClaimRoles.ADMIN))
            {
                var idClaim = User.Claims.FirstOrDefault(c => c.Type == "Id");
                if (idClaim != null)
                {
                    var value = int.Parse(idClaim.Value);
                    if (value == id)
                    {
                        return StatusCode(StatusCodes.Status403Forbidden, new Respone()
                        {Status = false,
                            Message = "You can't ban yoursefl.",
                            Data = null,
                        }); ;
                    }
                }
            }
            if (id <= 0) return BadRequest(new Respone()
            {Status = false,
                Message = "Bad request!",
            });
            var result = await _employeeService.Ban(id);
            if (result == null) return NotFound(new Respone()
            {
                Status = false,
                Message = "Not found!"
            });
            else if (result == false) return BadRequest(new Respone()
            {
                Status = false,
                Message = "Bad request!",
            });
            else return Ok(new Respone()
            {
                Status = true,
                Message = "Success!",
            });
        }
        [HttpPut("Unban/{id}")]
        [Authorize(Roles = ClaimRoles.ADMIN)]

        public async Task<ActionResult<Respone>> UnBanEmployee(int id, [FromBody] DtoEmployee dtoEmployee)
        {
            if (User.IsInRole(ClaimRoles.ADMIN))
            {
                var idClaim = User.Claims.FirstOrDefault(c => c.Type == "Id");
                if (idClaim != null)
                {
                    var value = int.Parse(idClaim.Value);
                    if (value == id)
                    {
                        return StatusCode(StatusCodes.Status403Forbidden, new Respone()
                        {
                            Status = false,
                            Message = "You can't unban yoursefl.",
                            Data = null,
                        }); ;
                    }
                }
            }
            if (id <= 0) return BadRequest(new Respone()
            {Status = false,
                Message = "Bad request!",
            });
            var result = await _employeeService.UnBan(id);
            if (result == null) return NotFound(new Respone()
            {
                Status = false,
                Message = "Not found!"
            });
            else if (result == false) return BadRequest(new Respone()
            {
                Status = false,
                Message = "Bad request!",
            });
            else return Ok(new Respone()
            {
                Status = true,
                Message = "Success!",
            });
        }
        //private bool EmployeeExists(int id)
        //{
        //    var check = _unitOfWork.Employees.GetById(id);
        //    if (check == null)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}
    }
}
