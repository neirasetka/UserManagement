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
            CreateMap<Permission, GetPermissionDto>().ReverseMap();
        }
    }
}