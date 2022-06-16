using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Core.Entities
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Name{ get; set; }
        public string LicensePlate { get; set; }
        public string Manufacturer { get; set; }
        public bool IsDeleted { get; set; } = false;
        public List<Expense> Expenses { get; set; }
        public User User { get; set; }
    }
}
