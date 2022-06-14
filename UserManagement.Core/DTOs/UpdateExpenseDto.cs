using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Core.Entities;

namespace UserManagement.Core.DTOs
{
    public class UpdateExpenseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public bool IsDeleted { get; set; } = false;
        public Vehicle Vehicle { get; set; }
        public DateTime ExpirationDate{ get; set; }
    }
}
