using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserManagement.Database;
﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Core.DTOs;
using UserManagement.Services.Interfaces;

namespace UserManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {

        private readonly IVehicleService _vehicleService;
        
        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllVehicles(int? pageNumber, int? pageSize, string? sortParametar, string? searchQuery)
        {
            var response = await _service.GetAllVehicles(pageNumber, pageSize, sortParametar, searchQuery); 
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddVehicle(AddVehicleDto newVehicle)
        {
            var response = await _vehicleService.AddVehicle(newVehicle);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var response = await _vehicleService.DeleteVehicle(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);

        }
        [HttpPut]
        public async Task<IActionResult> UpdateVehicle(UpdateVehicleDto updatedVehicle)
        {
            var response = await _vehicleService.UpdateVehicle(updatedVehicle);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicleById(int id)
        {
            var response = await _vehicleService.GetVehicleById(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }

}

