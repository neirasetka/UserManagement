using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserManagement.Database;
using UserManagement.Services.Interfaces;

namespace UserManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _service;

        public VehicleController(IVehicleService service)
        {
            _service = service;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllVehicles(int? pageNumber, int? pageSize, string? sortParametar, string? searchQuery)
        {
            var response = await _service.GetAllVehicles(pageNumber, pageSize, sortParametar, searchQuery); 
            if(response == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
