namespace UserManagement.Core.Entities
{
    public class Expense
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public bool IsDeleted { get; set; } = false;
        public Vehicle Vehicle { get; set; } 
    }
}