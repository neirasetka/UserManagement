using System.Collections.Generic;

namespace UserManagement.Core.Entities
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LicensePlate { get; set; }
        public string Manufacturer { get; set; }
        public bool IsDeleted { get; set; } = false;
        public List<Expense> Expenses { get; set; }
        public User User { get; set; }
    }
}