using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public VehicleService(UserManagementDbContext context, IMapper mapper)
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
    }
}