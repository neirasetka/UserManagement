﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Core.DTOs.UserDto;
using UserManagement.Core.Entities;
using UserManagement.Database;
using UserManagement.Services.Interfaces;

namespace UserManagement.Services.Services
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManagementDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthRepository(UserManagementDbContext context, IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
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
            else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Wrong password";
            }
            else
            {
                response.Data = CreateToken(user);
            }
            return response;
        }
        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            ServiceResponse<int> response = new ServiceResponse<int>();


            if(await UserExists(user.Username))

            {
                response.Success = false;
                response.Message = "User already exists!";
                return response;
            }
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Add(user);
            await _context.SaveChangesAsync();
            response.Data = user.Id;
            return response;
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(u => u.Username.ToLower().Equals(username.ToLower())))
            {
                return true;
            }
            return false;
        }
        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value)
            );

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public async Task<ServiceResponse<GetUserDto>> UpdateUserPassword(string username, string oldPassword, string newPassword)
        {
           
                var response = new ServiceResponse<GetUserDto>();
                try
                {
                    User user = await _context.Users
                        .FirstOrDefaultAsync(c => c.Username == username && !c.IsDeleted);
                    if (user == null)
                    {
                        response.Success = false;
                        response.Message = "User not found!";
                        return response;
                    }
           
                if (!VerifyPasswordHash(oldPassword,user.PasswordHash,user.PasswordSalt))
                {
                    response.Success = false;
                    response.Message = "Old password is not correct!";
                    return response;
                }
                else
                {
                    CreatePasswordHash(newPassword, out byte[] passwordHashNew, out byte[] passwordSaltNew);
                    user.PasswordHash = passwordHashNew;
                    user.PasswordSalt = passwordSaltNew;

                }


                await _context.SaveChangesAsync();
                    response.Data = _mapper.Map<GetUserDto>(user);
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Message = ex.Message;
                }
                return response;
            
        }
    }
}