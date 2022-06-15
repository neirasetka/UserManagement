namespace UserManagement.Core.DTOs.PermissionDto
{
    public class UpdatePermissionDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
    }
}