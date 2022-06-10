using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Core.DTOs;

using UserManagement.Core.Entities;
using UserManagement.Services.Interfaces;

namespace UserManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetUserDto>>>> AddUser(AddUserDto newUser)
        {
            return Ok(await _userService.AddUser(newUser));
        }
        [HttpPost("Permission")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> AddPermissionToUser(AddPermissionToUserDto newPermission)
        {
            return Ok(await _userService.AddPersmissionToUser(newPermission));
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetUserDto>>>> DeleteUser(int id)
        {
            var response = await _userService.DeleteUser(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);

        }

        [HttpGet("GetAll")]

        public async Task<ActionResult<ServiceResponse<List<GetUserDto>>>> Get()
        {
            return Ok(await _userService.GetAllUsers());
        }

        [HttpGet("FilterByStatus")]

        public async Task<ActionResult<ServiceResponse<List<GetUserDto>>>> FilterByStatus(string filter)
        {
            return Ok(await _userService.FilterUsers(filter));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> UpdateCharacter(UpdateUserDto updatedUser)
        {

            var response = await _userService.UpdateUser(updatedUser);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}