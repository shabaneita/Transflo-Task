using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Transflo_Task.Data;
using Transflo_Task.Models;
using Transflo_Task.Models.Dto;
using Transflo_Task.Repository.IRepostiory;

namespace Transflo_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IDriverRepository _context;
        private readonly IMapper _mapper;
        public DriversController(IDriverRepository context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _response = new();

        }

        // GET: api/Drivers
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetDrivers([FromQuery] string? search, int pageSize = 0, int pageNumber = 1)
        {
            try
            {

                IEnumerable<Driver> driverList;

                driverList = await _context.GetAllAsync(pageSize: pageSize,
                    pageNumber: pageNumber);

                if (!string.IsNullOrEmpty(search))
                {
                    driverList = driverList.Where(u => u.FirstName.ToLower().Contains(search)||u.LastName.ToLower().Contains(search)|| u.PhoneNumber.ToLower().Contains(search)|| u.Email.ToLower().Contains(search));
                }
                Pagination pagination = new() { PageNumber = pageNumber, PageSize = pageSize };

                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));
                _response.Result = _mapper.Map<List<DriverDTO>>(driverList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        // GET: api/Drivers/1
        [HttpGet("{id:int}", Name = "GetDriver")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetDriver(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var driver = await _context.GetAsync(u => u.Id == id);
                if (driver == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<DriverDTO>(driver);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        // PUT: api/Drivers/5
        [HttpPut("{id:int}", Name = "UpdateDriver")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateDriver(int id, [FromBody] DriverUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.Id)
                {
                    return BadRequest();
                }

                Driver model = _mapper.Map<Driver>(updateDTO);

                await _context.UpdateAsync(model);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateDriver([FromBody] DriverCreateDTO createDTO)
        {
            try
            {

                if (await _context.GetAsync(u => u.Email.ToLower() == createDTO.Email.ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "email of driver already Exists!");
                    return BadRequest(ModelState);
                }

                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                Driver driver = _mapper.Map<Driver>(createDTO);
                await _context.CreateAsync(driver);
                _response.Result = _mapper.Map<DriverDTO>(driver);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetDriver", new { id = driver.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }







        // DELETE: api/Drivers/5
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:int}", Name = "DeleteDriver")]
        public async Task<ActionResult<APIResponse>> DeleteDriver(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var driver = await _context.GetAsync(u => u.Id == id);
                if (driver == null)
                {
                    return NotFound();
                }
                await _context.RemoveAsync(driver);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

      
    }
}
