using Microsoft.EntityFrameworkCore;
using UserManagement.Core.Entities;

namespace UserManagement.Database
{
    public class UserManagementDbContext : DbContext
    {
        public UserManagementDbContext(DbContextOptions<UserManagementDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Expense> Expenses { get; set; }

    }
}