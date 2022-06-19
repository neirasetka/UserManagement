using System;

namespace UserManagement.Core.DTOs.ExpenseDto
{
    public class GetExpenseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int VehicleId{ get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}