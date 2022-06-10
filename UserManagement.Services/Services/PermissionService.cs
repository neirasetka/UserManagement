using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

        public PermissionService (UserManagementDbContext context, IMapper mapper)
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

        public async Task<ServiceResponse<List<GetPermissionDto>>> AddPermission(AddPermissionDto newPermission)
        {
            var response = new ServiceResponse<List<GetPermissionDto>>();
            var permission = _mapper.Map<Permission>(newPermission);
            _context.Permissions.Add(permission);
            await _context.SaveChangesAsync();
            response.Data = await _context.Permissions.Select(u => _mapper.Map<GetPermissionDto>(u)).ToListAsync();
            return response;
        }

        public async Task<ServiceResponse<List<GetPermissionDto>>> DeletePermission(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetPermissionDto>>();
            try
            {
                Permission permission = await _context.Permissions.FirstOrDefaultAsync(u => u.Id == id&& !u.IsDeleted);
                permission.IsDeleted = true;

                await _context.SaveChangesAsync();

                serviceResponse.Data = _context.Permissions
                                   .Select(u => _mapper.Map<GetPermissionDto>(u)).ToList();
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