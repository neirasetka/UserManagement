using UserManagement.Common.Enums;

namespace UserManagement.Core.DTOs.UserDto
{
    public class UpdateUserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public Status UserStatus { get; set; }
        public bool IsDeleted { get; set; }
    }
}