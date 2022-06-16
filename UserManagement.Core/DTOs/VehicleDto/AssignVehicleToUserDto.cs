using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Core.DTOs.VehicleDto
{
    public class AssignVehicleToUserDto
    {
        public int UserId { get; set; }
        public int VehicleId { get; set; }
    }
}
