using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Core.Entities
{
    public class Permission
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public List<User> Users { get; set; } = new List<User>();
    }
}
