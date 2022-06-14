
﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using UserManagement.Core.Entities;
using UserManagement.Database;
using UserManagement.Services.Interfaces;

namespace UserManagement.Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManagementDbContext _context;

        public AuthService(UserManagementDbContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse<string>> Register(User newUser)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            if (await UserExists(newUser.Username))
            {
                response.Success = false;
                response.Message = "User already exists!";
                return response;
            }
            _context.Add(newUser);
            await _context.SaveChangesAsync();
            response.Data = newUser.Username;
            return response;
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(u => u.Username.ToLower().Equals(username.ToLower())))
            {
                return true;
            }
            return false;
        }
    }
}
