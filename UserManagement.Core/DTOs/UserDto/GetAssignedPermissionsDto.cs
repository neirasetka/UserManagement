using System.Collections.Generic;
using UserManagement.Common.Enums;

namespace UserManagement.Core.DTOs.PermissionDto
{
    public class GetAssignedPermissionsDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public Status UserStatus { get; set; }
        public List<GetPermissionDto> Permissions { get; set; }
    }
}
