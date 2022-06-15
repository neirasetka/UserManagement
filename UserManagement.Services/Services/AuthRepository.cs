using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Core.Entities;
using UserManagement.Database;
using UserManagement.Services.Interfaces;

namespace UserManagement.Services.Services
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManagementDbContext _context;
        private readonly IConfiguration _configuration;
        public AuthRepository(UserManagementDbContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task<ServiceResponse<string>> Login(string username, string password)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            User user = await _context.Users.FirstOrDefaultAsync(x => x.Username.ToLower().Equals(username.ToLower()));
            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found.";
            }
            else 
            {
                response.Success = false;
                response.Message = "Wrong password";
            }
            return response;
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