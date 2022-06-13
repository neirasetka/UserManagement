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
        public async Task<IActionResult> AddUser(AddUserDto newUser)
        {
            return Ok(await _userService.AddUser(newUser));
        }

        [HttpPost("Permission")]
        public async Task<IActionResult> AddPermissionToUser(AddPermissionToUserDto newPermission)
        {
            return Ok(await _userService.AddPersmissionToUser(newPermission));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var response = await _userService.DeleteUser(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);

        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get(int? pageNumber, int? pageSize, string? sortParametar, string? searchQuery, string? filterParameter)
        {
            return Ok(await _userService.GetAllUsers(pageNumber,pageSize, sortParametar, searchQuery, filterParameter));
        }

        [HttpGet("FilterByStatus")]

        public async Task<IActionResult> FilterByStatus(string filter)
        {
            return Ok(await _userService.FilterUsers(filter));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCharacter(UpdateUserDto updatedUser)
        {

            var response = await _userService.UpdateUser(updatedUser);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpGet("GetSort")]
        public async Task<IActionResult> SortUsers(string parameter)
        {

            var response = await _userService.sortUsers(parameter);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }












    }
}