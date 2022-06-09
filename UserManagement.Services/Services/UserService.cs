using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Core.DTOs;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserManagement.Core.DTOs.User;
using UserManagement.Core.Entities;
using UserManagement.Database;
using UserManagement.Services.Interfaces;

namespace UserManagement.Services.Services
{
    public class UserService : IUserService
    {
        private readonly UserManagementDbContext _context;
        private readonly IMapper _mapper;

        public UserService(UserManagementDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<GetUserDto>> AddPersmissionToUser(AddPermissionToUserDto permission)
        {
            var response = new ServiceResponse<GetUserDto>();
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == permission.UserId);
                if(user == null)
                {
                    response.Success = false;
                    response.Message = "User not found!";
                    return response;
                }
                var perm = await _context.Permissions.FirstOrDefaultAsync(p => p.Id == permission.PermissionId);
                if(perm == null)
                {
                    response.Success = false;
                    response.Message = "Permission not found!";
                    return response;
                }
                user.Permissions.Add(perm);
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetUserDto>(user);
            }
            catch(Exception ex)
            {
                response.Success=false;
                response.Message = ex.Message;
            }
            return response;

        }

        public async Task<ServiceResponse<List<GetUserDto>>> AddUser(AddUserDto newUser)
        {
            var response = new ServiceResponse<List<GetUserDto>>();
            var user = _mapper.Map<User>(newUser);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            response.Data = await _context.Users.Select(u => _mapper.Map<GetUserDto>(u)).ToListAsync();
            return response;
        }

        public async Task<ServiceResponse<List<GetUserDto>>> DeleteUser(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetUserDto>>();
            try
            {
                User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id&&!u.IsDeleted);
                user.IsDeleted = true;

                await _context.SaveChangesAsync();

                serviceResponse.Data = _context.Users
                                   .Select(u => _mapper.Map<GetUserDto>(u)).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
  


        public async Task<ServiceResponse<List<GetUserDto>>> GetAllUsers()
        {
            var serviceResponse = new ServiceResponse<List<GetUserDto>>();
            var dbUsers = await _context.Users.Where(c => c.isDeleted == false).ToListAsync();
            serviceResponse.Data = dbUsers.Select(c => _mapper.Map<GetUserDto>(c)).ToList();
            return serviceResponse;
        }
       

        public async Task<ServiceResponse<GetUserDto>> UpdateUser(UpdateUserDto updatedUser)
        {
            var serviceResponse = new ServiceResponse<GetUserDto>();
            try
            {
                User user = await _context.Users
                    .FirstOrDefaultAsync(c => c.Id == updatedUser.Id);
                if (user == null)
                {
                    throw new Exception("User not found");
                }
                else {
                    user.FirstName = updatedUser.FirstName;
                    user.LastName = updatedUser.LastName;
                    user.Username = updatedUser.Username;
                    user.Email = updatedUser.Email;
                    user.UserStatus = updatedUser.UserStatus;
                    user.isDeleted = updatedUser.isDeleted;
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = _mapper.Map<GetUserDto>(user);
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}