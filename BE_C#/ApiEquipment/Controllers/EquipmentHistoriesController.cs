using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiEquipment.Entities;
using ApiEquipment.Interfaces;
using ApiEquipment.Helpers;
using ApiEquipment.Dto;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using ApiEquipment.Services.Interfaces;
using ApiEquipment.Services;
using ApiEquipment.GlobalClass;

namespace ApiEquipment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentHistoriesController : ControllerBase
    {
        private readonly IEquipmentHistoryService _equipmentHistoryService;

        public EquipmentHistoriesController(IEquipmentHistoryService equipmentHistoryService)
        {
            _equipmentHistoryService = equipmentHistoryService;
        }

        // GET: api/EquipmentHistories
        [HttpGet]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<Respone>> GetEquipmentHistories()
        {
            try
            {
                var result = await _equipmentHistoryService.GetAll();
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

        // GET: api/EquipmentHistories/5
        [HttpGet("{id}")]
        [Authorize]

        public async Task<ActionResult<Respone>> GetEquipmentHistory(int id)
        {
            
            if (id <= 0) return BadRequest(new Respone()
            {Status = false,
                Message = "Bad request!",
            });
            try
            {
                var result = await _equipmentHistoryService.GetById(id);
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
                    Status = false, Message = "An error occurred while processing your request.",
                });
            }
        }
        [HttpGet("EmployeeId/{id}")]
        [Authorize]

        public async Task<ActionResult<Respone>> GetEquipmentHistoryByEmployeeId(int id)
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
                        {Status = false,
                            Message = "You do not have permission to access this resource.",
                            Data = null,
                        }); ;
                    }
                }
            }
            if (id <= 0) return BadRequest(new Respone()
            {Status = false,
                Message = "Bad request!",
            });
            try
            {
                var result = await _equipmentHistoryService.GetByEmployeeId(id);
                if (result == null)
                {
                    return NotFound(new Respone()
                    {Status = false,
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
                    Status = false, Message = "An error occurred while processing your request.",
                });
            }
        }
        [HttpGet("EquipmentAssignedByEmployeeId/{id}")]
        [Authorize]

        public async Task<ActionResult<Respone>> GetEquipmentAssignedByEmployeeId(int id)
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
                        {Status = false,
                            Message = "You do not have permission to access this resource.",
                            Data = null,
                        }); ;
                    }
                }
            }
            if (id <= 0) return BadRequest(new Respone()
            {Status = false,
                Message = "Bad request!",
            });
            try
            {
                var result = await _equipmentHistoryService.GetEquipmentAssignedByEmployeeId(id);
                if (result == null)
                {
                    return NotFound(new Respone()
                    {Status = false,
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
                    Status = false, Message = "An error occurred while processing your request.",
                });
            }
        }
        // PUT: api/EquipmentHistories/5

        [HttpPut("{id}")]
        [Authorize(Roles = ClaimRoles.USER)]

        public async Task<ActionResult<Respone>> PutEquipmentHistory(int id, [FromBody] DtoEquipmentHistory dtoEquipmentHistory)
        {
            if (id <= 0 || dtoEquipmentHistory == null) return BadRequest(new Respone()
            {
                Status = false,

                Message = "Bad request!",
            });
            try
            {
                var result = await _equipmentHistoryService.Update(id, dtoEquipmentHistory);
                if (result == null) return NotFound(new Respone()
                {Status = false,
                    Message = "Not found!"
                });

                else if (result == false)
                {
                    return BadRequest(new Respone()
                    {Status = false,
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
        // POST: api/EquipmentHistories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = ClaimRoles.USER)]

        public async Task<ActionResult<Respone>> PostEquipmentHistory([FromBody] DtoEquipmentHistory dtoEquipmentHistory)
        {
            if (dtoEquipmentHistory == null)
            {
                return BadRequest(new Respone()
                {
                    Status = false,

                    Message = "Bad request!",
                });
            }
            try
            {
                var result = await _equipmentHistoryService.Add(dtoEquipmentHistory);
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

        // DELETE: api/EquipmentHistories/5
        [HttpDelete("{id}")]
        [Authorize(Roles = ClaimRoles.ADMIN)]

        public async Task<ActionResult<Respone>> DeleteEquipmentHistory(int id)
        {
            if (id <= 0) return BadRequest(new Respone()
            {Status = false,
                Message = "Bad request!",
            });
            try
            {
                var result = await _equipmentHistoryService.Delete(id);
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
        [HttpPut("Approve/{id}")]
        [Authorize(Roles = ClaimRoles.ADMIN)]

        public async Task<ActionResult<Respone>> ApproveEquipmentHistory(int id,[FromBody] DtoEquipmentHistory dtoEquipmentHistory )
        {
            if (id <= 0) return BadRequest(new Respone()
            {Status = false,
                Message = "Bad request!",
            });
            try
            {
                var result = await _equipmentHistoryService.Approve(id);
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
        [HttpPut("CheckReturn/{id}")]
        [Authorize(Roles = ClaimRoles.ADMIN)]
        public async Task<ActionResult<Respone>> CheckReturnEquipmentHistory(int id, [FromBody] DtoEquipmentHistory dtoEquipmentHistory)
        {
            if (id <= 0) return BadRequest(new Respone()
            {
                Status = false,

                Message = "Bad request!",
            });
            try
            {
                var result = await _equipmentHistoryService.CheckReturn(id);
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
