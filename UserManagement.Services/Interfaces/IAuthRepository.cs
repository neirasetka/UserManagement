using System.Threading.Tasks;
using UserManagement.Core.DTOs.UserDto;
using UserManagement.Core.Entities;

namespace UserManagement.Services.Interfaces
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<ServiceResponse<string>> Login(string username, string password);
        Task<bool> UserExists(string username);

        Task<ServiceResponse<GetUserDto>> UpdateUserPassword(string username, string oldPassword, string newPassword);
    }
}