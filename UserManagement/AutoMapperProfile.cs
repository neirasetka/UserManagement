using AutoMapper;
using UserManagement.Core.DTOs.ExpenseDto;
using UserManagement.Core.DTOs.PermissionDto;
using UserManagement.Core.DTOs.UserDto;
using UserManagement.Core.DTOs.VehicleDto;
using UserManagement.Core.Entities;

namespace UserManagement.Mapper
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
            CreateMap<AddVehicleDto, Vehicle>().ReverseMap();
            CreateMap<AddExpenseDto, Expense>().ReverseMap();
            CreateMap<GetVehicleDto, Vehicle>().ReverseMap();
            CreateMap<GetExpenseDto, Expense>().ReverseMap();
        }
    }
}