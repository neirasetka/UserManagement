using System.Threading.Tasks;
using UserManagement.Core.DTOs;
using UserManagement.Core.Entities;

namespace UserManagement.Services.Interfaces
{
    public interface IPermissionService
    {
        Task<ServiceResponse<GetPermissionDto>> UpdatePermission(UpdatePermissionDto updatedPermission);
    }
}