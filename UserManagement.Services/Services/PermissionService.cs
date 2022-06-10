using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using UserManagement.Core.DTOs;
using UserManagement.Core.Entities;
using UserManagement.Database;
using UserManagement.Services.Interfaces;

namespace UserManagement.Services.Services
{
    public class PermissionService:IPermissionService
    {
        private readonly UserManagementDbContext _context;
        private readonly IMapper _mapper;
        public PermissionService(UserManagementDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<GetPermissionDto>> UpdatePermission(UpdatePermissionDto updatedPermission)
        {
            var serviceResponse = new ServiceResponse<GetPermissionDto>();
            try
            {
                Permission permission = await _context.Permissions
                    .FirstOrDefaultAsync(c => c.Id == updatedPermission.Id);
                if (permission == null)
                {
                    throw new Exception("Permission not found");
                }
                else
                {
                    permission.Code = updatedPermission.Code;
                    permission.Description = updatedPermission.Description;
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = _mapper.Map<GetPermissionDto>(permission);
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