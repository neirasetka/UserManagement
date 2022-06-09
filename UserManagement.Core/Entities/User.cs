using System;
using System.Collections.Generic;
using UserManagement.Common.Enums;

namespace UserManagement.Core.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public string Email { get; set; }
        public bool IsDeleted { get; set; }

        public Status UserStatus { get; set; } = Status.Active;

        public List<Permission> Permissions { get; set; } = new List<Permission>();
        public Boolean isDeleted { get; set; } = false;
    }
}