using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Core.DTOs.User;
using UserManagement.Core.Entities;
using UserManagement.Services.Interfaces;

namespace UserManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetUserDto>>>> DeleteUser(int id)
        {
            var response = await _userService.DeleteUser(id);
            if(response.Data==null)
            

        public UserController (IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetAll")]

        public async Task<ActionResult<ServiceResponse<List<GetUserDto>>>> Get()
        {
           // int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            return Ok(await _userService.GetAllUsers());
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
