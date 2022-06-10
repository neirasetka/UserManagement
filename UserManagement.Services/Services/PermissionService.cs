using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Core.DTOs;
using UserManagement.Core.Entities;
using UserManagement.Database;
using UserManagement.Services.Interfaces;

namespace UserManagement.Services.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly UserManagementDbContext _context;
        private readonly IMapper _mapper;

        public PermissionService(UserManagementDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<GetAssignedPermissionsDto>> ViewAssignedPermissions(int id)
        {
            var response = new ServiceResponse<GetAssignedPermissionsDto>();
            try
            {
                var user = await _context.Users.Include(u => u.Permissions).FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);
                if (user == null)
                {
                    response.Success = false;
                    response.Message = "User not found!";
                    return response;
                }
                response.Data = _mapper.Map<GetAssignedPermissionsDto>(user);
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
