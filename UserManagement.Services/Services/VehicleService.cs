using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Common.Enums;
using UserManagement.Core.DTOs;
using UserManagement.Core.Entities;
using UserManagement.Database;
using UserManagement.Services.Interfaces;

namespace UserManagement.Services.Services
{
    public class VehicleService:IVehicleService
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

            if(searchQuery != null)
                dbVehicles = dbVehicles.Where((u=> u.Name.ToLower().Contains(searchQuery.ToLower()) 
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
                        dbVehicles= dbVehicles.OrderBy(q => q.Manufacturer).ToList();
                        break;
                    default:  
                        break;
                }
            }
            
            var currentPageNumber = pageNumber ?? 1;
            var currentPageSize = pageSize ?? 10;
            response.Data = dbVehicles.Skip((currentPageNumber-1)*currentPageSize).Take(currentPageSize).Select(c => _mapper.Map<GetVehicleDto>(c)).ToList();
            return response;
        }      
    }
}
