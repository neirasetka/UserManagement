﻿namespace UserManagement.Core.DTOs
{
    public class GetPermissionDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}