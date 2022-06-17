using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Core.DTOs.VehicleDto;
using UserManagement.Core.Entities;

namespace UserManagement.Services.Interfaces
{
    public interface IVehicleService
    {
        Task<ServiceResponse<List<GetVehicleDto>>> GetAllVehicles(int? pageNumber, int? pageSize, string? sortParametar, string? searchQuery);
        Task<ServiceResponse<List<GetVehicleDto>>> AddVehicle(AddVehicleDto newVehicle);
        Task<ServiceResponse<List<GetVehicleDto>>> DeleteVehicle(int id);
        Task<ServiceResponse<GetVehicleDto>> UpdateVehicle(UpdateVehicleDto updatedVehicle);
        Task<ServiceResponse<GetVehicleDto>> GetVehicleById(int id);
        Task<ServiceResponse<GetVehicleDto>> AssignVehicleToUser(AssignVehicleToUserDto request);
    }
}