using System.Collections.Generic;
using UserManagement.Core.Entities;

namespace UserManagement.Core.DTOs.VehicleDto
{
    public class UpdateVehicleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LicensePlate { get; set; }
        public string Manufacturer { get; set; }
        public bool IsDeleted { get; set; }
        public List<Expense> Expenses { get; set; }
    }
}