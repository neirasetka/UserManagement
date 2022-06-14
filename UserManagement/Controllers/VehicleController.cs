using System.Threading.Tasks;
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
