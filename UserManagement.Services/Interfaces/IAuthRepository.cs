using System.Threading.Tasks;
using UserManagement.Core.Entities;

namespace UserManagement.Services.Interfaces
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<string>> Login(string username, string password);
        Task<ServiceResponse<int>> Register(User user, string password);


    }
}