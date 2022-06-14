using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserManagement.Core.DTOs;
using UserManagement.Core.Entities;
using UserManagement.Services.Interfaces;

namespace UserManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterDto newUser)
        {
            var response = await _service.Register(new User { Username = newUser.Username, Password = newUser.Password, Email = newUser.Email, LastName = newUser.LastName, FirstName = newUser.FirstName}); 
            if(response == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
