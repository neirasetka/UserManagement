using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserManagement.Core.DTOs.UserDto;
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

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get(int? pageNumber, int? pageSize, string? sortParametar, string? searchQuery, string? filterParameter)
        {
            var response = await _userService.GetAllUsers(pageNumber, pageSize, sortParametar, searchQuery, filterParameter);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserDto newUser)
        {
            var response = await _userService.AddUser(newUser);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPost("Permission")]
        public async Task<IActionResult> AddPermissionToUser(AddPermissionToUserDto newPermission)
        {
            var response = await _userService.AddPersmissionToUser(newPermission);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
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
    }
}