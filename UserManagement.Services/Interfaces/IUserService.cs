using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Core.DTOs.User;
using UserManagement.Core.Entities;

namespace UserManagement.Services.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResponse<List<GetUserDto>>> DeleteUser(int id);
    }
}
