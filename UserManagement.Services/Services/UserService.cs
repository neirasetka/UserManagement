using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Common.Enums;
using UserManagement.Core.DTOs.UserDto;
using UserManagement.Core.Entities;
using UserManagement.Database;
using UserManagement.Services.Interfaces;

namespace UserManagement.Services.Services
{
    public class UserService : IUserService
    {
        private readonly UserManagementDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAuthRepository _authRepository;

        public UserService(UserManagementDbContext context, IMapper mapper, IAuthRepository authRepository)
        {
            _context = context;
            _mapper = mapper;
            _authRepository = authRepository;
        }
        

        public async Task<ServiceResponse<GetUserDto>> AddPersmissionToUser(AddPermissionToUserDto newPermission)
        {
            var response = new ServiceResponse<GetUserDto>();
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == newPermission.UserId);
                if (user == null)
                {
                    response.Success = false;
                    response.Message = "User not found!";
                    return response;
                }

                var permission = await _context.Permissions.FirstOrDefaultAsync(p => p.Id == newPermission.PermissionId);

                if (permission == null)
                {
                    response.Success = false;
                    response.Message = "Permission not found!";
                    return response;
                }
                user.Permissions.Add(permission);
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

        public async Task<ServiceResponse<List<GetUserDto>>> AddUser(UserRegisterDto newUser)
        {
            var response = new ServiceResponse<List<GetUserDto>>();
            var user = _mapper.Map<User>(newUser);
            await _authRepository.Register(user, newUser.Password);
            response.Data = await _context.Users.Select(u => _mapper.Map<GetUserDto>(u)).ToListAsync();
            return response;
        }

        public async Task<ServiceResponse<List<GetUserDto>>> DeleteUser(int id)
        {
            var response = new ServiceResponse<List<GetUserDto>>();
            try
            {
                User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);
                if (user == null)
                {
                    response.Success = false;
                    response.Message = "User not found!";
                    return response;
                }
                user.IsDeleted = true;

                await _context.SaveChangesAsync();

                response.Data = _context.Users
                                   .Select(u => _mapper.Map<GetUserDto>(u)).ToList();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<List<GetUserDto>>> GetAllUsers(int? pageNumber, int? pageSize, string? sortParametar, string? searchQuery, string filterParameter)
        {
            var response = new ServiceResponse<List<GetUserDto>>();

            var dbUsers = await _context.Users.Where(c => c.IsDeleted == false).ToListAsync();

            if (searchQuery != null)
                dbUsers = dbUsers.Where((u => u.FirstName.ToLower().Contains(searchQuery.ToLower())
                                          || u.LastName.ToLower().Contains(searchQuery.ToLower())
                                          || u.Username.ToLower().Contains(searchQuery.ToLower()))).ToList();

            if (filterParameter == "Active")
                dbUsers = dbUsers.Where(u => u.UserStatus == Status.Active).ToList();

            else if (filterParameter == "Inactive")
                dbUsers = dbUsers.Where(u => u.UserStatus == Status.Inactive).ToList();

            if (sortParametar != null)
            {
                switch (sortParametar)
                {
                    case "first_name":
                        dbUsers = dbUsers.OrderBy(q => q.FirstName).ToList();
                        break;
                    case "last_name":
                        dbUsers = dbUsers.OrderBy(q => q.LastName).ToList();
                        break;
                    case "username":
                        dbUsers = dbUsers.OrderBy(q => q.Username).ToList();
                        break;
                    default:
                        break;
                }
            }

            var currentPageNumber = pageNumber ?? 1;
            var currentPageSize = pageSize ?? 10;
            response.Data = dbUsers.Skip((currentPageNumber - 1) * currentPageSize).Take(currentPageSize).Select(c => _mapper.Map<GetUserDto>(c)).ToList();
            return response;
        }

       

        public async Task<ServiceResponse<GetUserDto>> UpdateUser(UpdateUserDto updatedUser)
        {
            var response = new ServiceResponse<GetUserDto>();
            try
            {
                User user = await _context.Users
                    .FirstOrDefaultAsync(c => c.Id == updatedUser.Id && !c.IsDeleted);
                if (user == null)
                {
                    response.Success = false;
                    response.Message = "User not found!";
                    return response;
                }
                user.FirstName = updatedUser.FirstName;
                user.LastName = updatedUser.LastName;
                user.Username = updatedUser.Username;
                user.Email = updatedUser.Email;
                user.UserStatus = updatedUser.UserStatus;
                user.IsDeleted = updatedUser.IsDeleted;
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

