using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserManagement.Core.DTOs.PermissionDto;
using UserManagement.Services.Interfaces;

namespace UserManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;
        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpGet("Assigned")]
        public async Task<IActionResult> ViewAssignedPermissions(int id)
        {
            var response = await _permissionService.ViewAssignedPermissions(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddPermission(AddPermissionDto newPermision)
        {
            var response = await _permissionService.AddPermission(newPermision);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePermission(UpdatePermissionDto updatedPermission)
        {
            var response = await _permissionService.UpdatePermission(updatedPermission);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePermission(int id)
        {
            var response = await _permissionService.DeletePermission(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}