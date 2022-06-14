using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using UserManagement.Core.DTOs;
using UserManagement.Core.Entities;
using UserManagement.Database;
using UserManagement.Services.Interfaces;
using System;

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

        public async Task<ServiceResponse<List<GetVehicleDto>>> AddVehicle(AddVehicleDto newVehicle)
        {
            var response = new ServiceResponse<List<GetVehicleDto>>();
            var vehicle = _mapper.Map<Vehicle>(newVehicle);
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();
            response.Data = await _context.Vehicles.Select(v => _mapper.Map<GetVehicleDto>(v)).ToListAsync();
            return response;
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

        public async Task<ServiceResponse<GetVehicleDto>> GetVehicleById(int id)
        {
            var response = new ServiceResponse<GetVehicleDto>();
            try
            {
                Vehicle vehicle = await _context.Vehicles
                    .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
                if (vehicle == null)
                {
                    response.Success = false;
                    response.Message = "Vehcile not found!";
                    return response;
                }
                
                response.Data = _mapper.Map<GetVehicleDto>(vehicle);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<GetVehicleDto>> UpdateVehicle(UpdateVehicleDto updatedVehicle)
        {
            var response = new ServiceResponse<GetVehicleDto>();
            try
            {
                Vehicle vehicle = await _context.Vehicles
                    .FirstOrDefaultAsync(c => c.Id == updatedVehicle.Id && !c.IsDeleted);
                if (vehicle == null)
                {
                    response.Success = false;
                    response.Message = "Vehcile not found!";
                    return response;
                }
                vehicle.Name = updatedVehicle.Name;
                vehicle.LicensePlate = updatedVehicle.LicensePlate;
                vehicle.Manufacturer = updatedVehicle.Manufacturer;
                vehicle.IsDeleted = updatedVehicle.IsDeleted;
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetVehicleDto>(vehicle);
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