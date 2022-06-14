using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Core.DTOs;
using UserManagement.Core.Entities;

namespace UserManagement.Services.Interfaces
{
    public interface IVehicleService
    {
        Task<ServiceResponse<List<GetVehicleDto>>> AddVehicle(AddVehicleDto newVehicle);
    }
}