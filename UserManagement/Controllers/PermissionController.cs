using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Core.DTOs;
using UserManagement.Core.Entities;
using UserManagement.Services.Interfaces;

namespace UserManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService  _permissionService;
        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetPermissionDto>>>> AddPermission(AddPermissionDto newPermision)
        {
        
            return Ok(await _permissionService.AddPermission(newPermision));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetPermissionDto>>>> DeletePermission(int id)
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
