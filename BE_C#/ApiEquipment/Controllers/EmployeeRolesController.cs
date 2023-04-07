//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using ApiEquipment.Entities;
//using ApiEquipment.Interfaces;
//using ApiEquipment.Dto;
//using ApiEquipment.Helpers;
//using static System.Net.WebRequestMethods;
//using Microsoft.AspNetCore.Authorization;
//using System.Data;
//using ApiEquipment.Services.Interfaces;
//using ApiEquipment.Services;
//using ApiEquipment.Repositories;
//using ApiEquipment.GlobalClass;
//namespace ApiEquipment.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class EmployeeRolesController : ControllerBase
//    {
//        private readonly IEmployeeRoleService _employeeRoleService;

//        public EmployeeRolesController(IEmployeeRoleService employeeRoleService)
//        {
//            _employeeRoleService = employeeRoleService;
//        }

//        [HttpGet]
//        [Authorize(Roles = ClaimRoles.ADMIN)]

//        public async Task<ActionResult<Respone>> GetEmployeeRoles()
//        {
//            try
//            {
//                var employeeRoles = await _employeeRoleService.GetAll();

//                return Ok(new Respone()
//                {
//                    Message = "Success!",
//                    Data = employeeRoles,
//                });
//            }
//            catch (Exception ex)
//            {

//                return StatusCode(StatusCodes.Status500InternalServerError, new Respone()
//                {
//                    Status = false,
//                    Message = "An error occurred while processing your request.",
//                });
//            }


//        }

//        // GET: api/EmployeeRoles/5
//        [HttpGet("{id}")]
//        [Authorize(Roles = ClaimRoles.ADMIN)]

//        public async Task<ActionResult<Respone>> GetEmployeeRole(int id)
//        {
//            if (id <= 0) return BadRequest(new Respone()
//            {
//                Status = false,
//                Message = "Bad request!",
//            });
//            try
//            {
//                var employeeRole = await _employeeRoleService.GetById(id);
//                if (employeeRole == null)
//                {
//                    return NotFound(new Respone()
//                    {
//                        Status = false,
//                        Message = "Not found!",
//                    });
//                }
//                return Ok(new Respone()
//                {
//                    Status = true,
//                    Message = "Success!",
//                    Data = employeeRole,
//                });
//            }
//            catch (Exception ex)
//            {

//                return StatusCode(StatusCodes.Status500InternalServerError, new Respone()
//                {
//                    Status = false,
//                    Message = "An error occurred while processing your request.",
//                });
//            }

//        }

//        // PUT: api/EmployeeRoles/5
//        [HttpPut("{id}")]
//        [Authorize(Roles = ClaimRoles.ADMIN)]

//        public async Task<ActionResult<Respone>> PutEmployeeRole(int id, [FromBody] DtoEmployeeRole dtoEmployeeRole)
//        {
//            if (id <= 0 || dtoEmployeeRole == null) return BadRequest(new Respone()
//            {
//                Status = false,
//                Message = "Bad request!",
//            });
//            try
//            {
//                var result = await _employeeRoleService.Update(id, dtoEmployeeRole);
//                if (result == null) return NotFound(new Respone()
//                {
//                    Status = false,
//                    Message = "Not found!"
//                });

//                else if (result == false)
//                {
//                    return BadRequest(new Respone()
//                    {

//                        Status = false,
//                        Message = "Bad request!",
//                    });
//                }
//                else return Ok(new Respone()
//                {
//                    Status = true,
//                    Message = "Success!",
//                });

//            }
//            catch (Exception ex)
//            {

//                return StatusCode(StatusCodes.Status500InternalServerError, new Respone()
//                {
//                    Status = false,
//                    Message = "An error occurred while processing your request.",
//                });
//            }
//        }
//        // POST: api/EmployeeRoles
//        [HttpPost]
//        [Authorize(Roles = ClaimRoles.ADMIN)]

//        public async Task<ActionResult<Respone>> PostEmployeeRole( [FromBody] DtoEmployeeRole dtoEmployeeRole)
//        {
//            if (dtoEmployeeRole == null)
//            {
//                return BadRequest(new Respone()
//                {
//                    Status = false,
//                    Message = "Bad request!",
//                });
//            }
//            try
//            {
//                var result = await _employeeRoleService.Add(dtoEmployeeRole);
//                if (result == null) return BadRequest(new Respone()
//                {
//                    Status = false,
//                    Message = "Bad request!",
//                });
//                return Ok(new Respone()
//                {
//                    Status = true,
//                    Message = "Success!",
//                    Data = result,
//                });
//            }
//            catch (Exception ex)
//            {

//                return StatusCode(StatusCodes.Status500InternalServerError, new Respone()
//                {
//                    Status = false,
//                    Message = "An error occurred while processing your request.",
//                });
//            }

//        }
//        // DELETE: api/EmployeeRoles/5
//        [HttpDelete("{id}")]
//        [Authorize(Roles = ClaimRoles.ADMIN)]

//        public async Task<ActionResult<Respone>> DeleteEmployeeRole(int id)
//        {
//            if (id <= 0) return BadRequest(new Respone()
//            {
//                Status = false,
//                Message = "Bad request!",
//            });
//            try
//            {
//                var result = await _employeeRoleService.Delete(id);
//                if (result == null) return NotFound(new Respone()
//                {
//                    Status = false,
//                    Message = "Not found!"
//                });
//                else if (result == false) return BadRequest(new Respone()
//                {
//                    Message = "Bad request!",
//                });
//                else return Ok(new Respone()
//                {
//                    Status = true,
//                    Message = "Success!",
//                });
//            }
//            catch (Exception ex)
//            {

//                return StatusCode(StatusCodes.Status500InternalServerError, new Respone()
//                {
//                    Status = false,
//                    Message = "An error occurred while processing your request.",
//                });
//            }
//        }
//    }
//}
