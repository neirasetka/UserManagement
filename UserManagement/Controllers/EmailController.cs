﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UserManagement.Services.Interfaces;

namespace UserManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(string to, string toName, string expenseName, DateTime date)
        {
            var response = await _emailService.SendEmail(to, toName, expenseName, date);
            if(response == null)
                return BadRequest(response);
            return Ok(response);
        }
    }
}