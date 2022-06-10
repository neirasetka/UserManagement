using System.Collections.Generic;

namespace UserManagement.Core.Entities
{
    public class Permission
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; } = false;
        public List<User> Users { get; set; } = new List<User>();
    }
}