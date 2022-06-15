﻿using Microsoft.AspNetCore.Http;
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
        private readonly IAuthRepository _authRepo;

        public AuthController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterDto newUser)
        {
            var response = await _authRepo.Register(
                new User { Username = newUser.Username }, newUser.Password
                );
            // var response = await _authRepo.Register(new User { Username = newUser.Username, /*Password = newUser.Password,*/ Email = newUser.Email, LastName = newUser.LastName, FirstName = newUser.FirstName });
            if (response == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDto request)
        {
            ServiceResponse<string> response = await _authRepo.Login(request.Username, request.Password);

            if (!response.Success)
            {
                return BadRequest(response);

            }
            return Ok(response);
        }
    }
}

