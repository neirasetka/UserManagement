﻿using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Core.DTOs.UserDto;
using UserManagement.Core.Entities;
namespace UserManagement.Services.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResponse<List<GetUserDto>>> AddUser(UserRegisterDto newUser);
        Task<ServiceResponse<GetUserDto>> AddPersmissionToUser(AddPermissionToUserDto permission);
        Task<ServiceResponse<List<GetUserDto>>> DeleteUser(int id);
        Task<ServiceResponse<List<GetUserDto>>> GetAllUsers(int? pageNumber, int? pageSize, string? sortParametar, string? searchQuery, string? filterParameter);
        Task<ServiceResponse<GetUserDto>> UpdateUser(UpdateUserDto updatedUser);
    }
}