using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Core.DTOs;
using UserManagement.Core.Entities;
namespace UserManagement.Services.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResponse<List<GetUserDto>>> AddUser(AddUserDto newUser);
        Task<ServiceResponse<GetUserDto>> AddPersmissionToUser(AddPermissionToUserDto permission);
        Task<ServiceResponse<List<GetUserDto>>> DeleteUser(int id);
        Task<ServiceResponse<List<GetUserDto>>> GetAllUsers(int? pageNumber,int? pageSize);
        Task<ServiceResponse<GetUserDto>> UpdateUser(UpdateUserDto updatedUser);
        Task<ServiceResponse<List<GetUserDto>>> FilterUsers(string filter);
        Task<ServiceResponse<List<GetUserDto>>> sortUsers(string parameter);
    }
}