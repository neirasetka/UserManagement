using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Core.DTOs;
using UserManagement.Core.Entities;

namespace UserManagement.Services.Interfaces
{
    public interface IVehicleService
    {
        Task<ServiceResponse<List<GetVehicleDto>>> DeleteVehicle(int id);
    }
}
