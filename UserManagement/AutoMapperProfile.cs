using AutoMapper;
using UserManagement.Core.DTOs;
using UserManagement.Core.Entities;

namespace UserManagement.API
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<User, GetUserDto>().ReverseMap();
            CreateMap<AddUserDto, User>().ReverseMap();
        }
    }
}