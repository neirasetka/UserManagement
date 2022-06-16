using System;
using UserManagement.Core.Entities;

namespace UserManagement.Core.DTOs.ExpenseDto
{
    public class AddExpenseDto
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int VehicleId{ get; set; }
        public DateTime ExpirationDate{ get; set; }
    }
}