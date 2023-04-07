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
using ApiEquipment.GlobalClass;

namespace ApiEquipment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentTypesController : ControllerBase
    {
        private readonly IEquipmentTypeService _equipmentTypeService;

        public EquipmentTypesController(IEquipmentTypeService equipmentTypeService)
        {
            _equipmentTypeService = equipmentTypeService;
        }

        // GET: api/Equipments
        [HttpGet]
        [Authorize]

        public async Task<ActionResult<Respone>> GetEquipmentTypes()
        {
            try
            {
                var result = await _equipmentTypeService.GetAll();
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
        [Authorize]

        public async Task<ActionResult<Respone>> GetEquipmentType(int id)
        {
            if (id <= 0) return BadRequest(new Respone()
            {
                Status = false,
                Message = "Bad request!",
            });
            try
            {
                var result = await _equipmentTypeService.GetById(id);
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

        public async Task<ActionResult<Respone>> PutEquipmentType(int id, [FromBody] DtoEquipmentType dtoEquipmentType)
        {
            if (id <= 0 || dtoEquipmentType == null) return BadRequest(new Respone()
            {Status = false,
                Message = "Bad request!",
            });
            try
            {
                var result = await _equipmentTypeService.Update(id, dtoEquipmentType);
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

        [HttpPost]
        [Authorize(Roles = ClaimRoles.ADMIN)]

        public async Task<ActionResult<Respone>> PostEquipmentType([FromBody] DtoEquipmentType dtoEquipmentType)
        {
            if (dtoEquipmentType == null)
            {
                return BadRequest(new Respone()
                {
                    Status = false,

                    Message = "Bad request!",
                });
            }
            try
            {
                var result = await _equipmentTypeService.Add(dtoEquipmentType);
                if (result == null) return BadRequest(new Respone()
                {Status = false,
                    Message = "Bad request!",
                });
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

        // DELETE: api/Equipments/5
        [HttpDelete("{id}")]
        [Authorize(Roles = ClaimRoles.ADMIN)]
        public async Task<ActionResult<Respone>> DeleteEquipmentType(int id)
        {
            if (id <= 0) return BadRequest(new Respone()
            {Status = false,
                Message = "Bad request!",
            });
            try
            {
                var result = await _equipmentTypeService.Delete(id);
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
