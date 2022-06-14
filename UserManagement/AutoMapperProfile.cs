using AutoMapper;
using UserManagement.Core.DTOs;
using UserManagement.Core.Entities;

namespace UserManagement
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<User, GetUserDto>().ReverseMap();
            CreateMap<AddUserDto, User>().ReverseMap();
            CreateMap<AddPermissionDto, Permission>().ReverseMap();
            CreateMap<GetPermissionDto, Permission>().ReverseMap();
            CreateMap<GetAssignedPermissionsDto, GetUserDto>().ReverseMap();
            CreateMap<GetAssignedPermissionsDto, User>().ReverseMap();
            CreateMap<GetAssignedPermissionsDto, Permission>().ReverseMap();
            CreateMap<GetAssignedPermissionsDto, GetPermissionDto>().ReverseMap();
            CreateMap<Permission, GetPermissionDto>().ReverseMap();
            CreateMap<Vehicle, GetVehicleDto>().ReverseMap();
            CreateMap<Expense, GetExpenseDto>().ReverseMap();
            CreateMap<GetVehicleDto, Vehicle>().ReverseMap();
        }
    }
}