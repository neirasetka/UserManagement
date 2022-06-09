using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Common.Enums;
using UserManagement.Core.Entities;

namespace UserManagement.Core.DTOs.User
{
    public class GetUserDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

       

        public string Email { get; set; }

        public Status UserStatus { get; set; } = Status.Active;

    }
}
