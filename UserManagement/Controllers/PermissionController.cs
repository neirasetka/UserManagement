﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserManagement.Core.DTOs;
using UserManagement.Core.Entities;
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

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetPermissionDto>>> UpdatePermission(UpdatePermissionDto updatedPermission)
        {
            var response = await _permissionService.UpdatePermission(updatedPermission);
            if(response.Data==null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}