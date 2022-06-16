using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Core.DTOs.VehicleDto;
using UserManagement.Core.Entities;
using UserManagement.Database;
using UserManagement.Services.Interfaces;

namespace UserManagement.Services.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly UserManagementDbContext _context;
        private readonly IMapper _mapper;

        public VehicleService(UserManagementDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetVehicleDto>>> GetAllVehicles(int? pageNumber, int? pageSize, string? sortParametar, string? searchQuery)
        {
            var response = new ServiceResponse<List<GetVehicleDto>>();

            var dbVehicles = await _context.Vehicles.Where(v => v.IsDeleted == false).ToListAsync();

            if (searchQuery != null)
                dbVehicles = dbVehicles.Where((u => u.Name.ToLower().Contains(searchQuery.ToLower())
                                          || u.LicensePlate.ToLower().Contains(searchQuery.ToLower())
                                          || u.Manufacturer.ToLower().Contains(searchQuery.ToLower()))).ToList();

            if (sortParametar != null)
            {
                switch (sortParametar)
                {
                    case "name":
                        dbVehicles = dbVehicles.OrderBy(q => q.Name).ToList();
                        break;
                    case "license_plate":
                        dbVehicles = dbVehicles.OrderBy(q => q.LicensePlate).ToList();
                        break;
                    case "manufacturer":
                        dbVehicles = dbVehicles.OrderBy(q => q.Manufacturer).ToList();
                        break;
                    default:
                        break;
                }
            }

            var currentPageNumber = pageNumber ?? 1;
            var currentPageSize = pageSize ?? 10;
            response.Data = dbVehicles.Skip((currentPageNumber - 1) * currentPageSize).Take(currentPageSize).Select(c => _mapper.Map<GetVehicleDto>(c)).ToList();
            return response;
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