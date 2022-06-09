using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagement.Core.DTOs
{
    public class AddUserDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }
    }
}
