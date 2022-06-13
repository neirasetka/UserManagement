using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserManagement.Core.DTOs;
using UserManagement.Core.Entities;
using UserManagement.Database;
using UserManagement.Services.Interfaces;

namespace UserManagement.Services.Services
{
    public class VehicleService : IVehicleService
    {


        private readonly UserManagementDbContext _context;
        private readonly IMapper _mapper;

        public VehicleService (UserManagementDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

   
        public async Task<ServiceResponse<List<GetVehicleDto>>> DeleteVehicle(int id)
        {
            var response = new ServiceResponse<List<GetVehicleDto>>();
            try
            {
              
                Vehicle vehicle = await _context.Vehicles.FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);
                if (vehicle == null)
                {
                    response.Success = false;
                    response.Message = "Vehicle not found!";
                    return response;
                }
                vehicle.IsDeleted = true;

                await _context.SaveChangesAsync();

                response.Data = _context.Vehicles
                                   .Select(u => _mapper.Map<GetVehicleDto>(u)).ToList();
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
