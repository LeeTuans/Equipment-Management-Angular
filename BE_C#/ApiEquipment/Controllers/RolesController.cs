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
using System.Data;
using Microsoft.AspNetCore.Authorization;
using ApiEquipment.Services.Interfaces;
using ApiEquipment.Services;
using ApiEquipment.GlobalClass;

namespace ApiEquipment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        [Authorize(Roles = ClaimRoles.ADMIN)]

        public async Task<ActionResult<Respone>> GetRoles()
        {
            try
            {
                var result = await _roleService.GetAll();
                return Ok(new Respone()
                {Status = true,
                    Message = "Success!",
                    Data = result,
                });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new Respone()
                {
                    Status = false, Message = "An error occurred while processing your request.",
                });
            }
        }

        //GET: api/Equipments/5
        [HttpGet("{id}")]
        [Authorize(Roles = ClaimRoles.ADMIN)]

        public async Task<ActionResult<Respone>> GetRole(int id)
        {
            if (id <= 0) return BadRequest(new Respone()
            {Status = false,
                Message = "Bad request!",
            });
            try
            {
                var result = await _roleService.GetById(id);
                if (result == null)
                {
                    return NotFound(new Respone()
                    {Status = false,
                        Message = "Not found!",
                    });
                }
                return Ok(new Respone()
                {Status = true,
                    Message = "Success!",
                    Data = result,
                });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new Respone()
                {
                    Status = false, Message = "An error occurred while processing your request.",
                });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = ClaimRoles.ADMIN)]

        public async Task<ActionResult<Respone>> PutRole(int id, [FromBody] DtoRole dtoRole)
        {
            if (id <= 0 || dtoRole == null) return BadRequest(new Respone()
            {Status = false,
                Message = "Bad request!",
            });
            try
            {
                var result = await _roleService.Update(id, dtoRole);
                if (result == null) return NotFound(new Respone()
                {
                    Message = "Not found!"
                });

                else if (result == false)
                {
                    return BadRequest(new Respone()
                    {
                        Status = false,
                        Message = "Bad request!",
                    });
                }
                else return Ok(new Respone()
                {
                    Status = true,
                    Message = "Success!",
                });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new Respone()
                {
                    Status = false, Message = "An error occurred while processing your request.",
                });
            }
        }

        [HttpPost]
        [Authorize(Roles = ClaimRoles.ADMIN)]

        public async Task<ActionResult<Respone>> PostRole([FromBody] DtoRole dtoRole)
        {
            if (dtoRole == null)
            {   
                return BadRequest(new Respone()
                {Status = false,
                    Message = "Bad request!",
                });
            }
            try
            {
                var result = await _roleService.Add(dtoRole);
                if (result == null) return BadRequest(new Respone()
                {Status = false,
                    Message = "Bad request!",
                });
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
                    Status = false, Message = "An error occurred while processing your request.",
                });
            }

        }

        // DELETE: api/Equipments/5
        [HttpDelete("{id}")]
        [Authorize(Roles = ClaimRoles.ADMIN)]
        public async Task<ActionResult<Respone>> DeleteRole(int id)
        {
            if (id <= 0) return BadRequest(new Respone()
            {Status = false,
                Message = "Bad request!",
            });
            try
            {
                var result = await _roleService.Delete(id);
                if (result == null) return NotFound(new Respone()
                {Status = false,
                    Message = "Not found!"
                });
                else if (result == false) return BadRequest(new Respone()
                {Status = false,
                    Message = "Bad request!",
                });
                else return Ok(new Respone()
                {
                    Status = true,

                    Message = "Success!",
                });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new Respone()
                {
                    Status = false, Message = "An error occurred while processing your request.",
                });
            }
        }

    }
}
