using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Core.DTOs;
using UserManagement.Core.Entities;

namespace UserManagement.Services.Interfaces
{
    public interface IPermissionService
    {
        Task<ServiceResponse<GetAssignedPermissionsDto>> ViewAssignedPermissions(int id);
        Task<ServiceResponse<GetPermissionDto>> UpdatePermission(UpdatePermissionDto updatedPermission);
        Task<ServiceResponse<List<GetPermissionDto>>> AddPermission(AddPermissionDto newPermission);
        Task<ServiceResponse<List<GetPermissionDto>>> DeletePermission(int id);
    }
}