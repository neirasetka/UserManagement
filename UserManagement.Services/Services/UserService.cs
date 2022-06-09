using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Core.DTOs;
using UserManagement.Core.Entities;
using UserManagement.Database;
using UserManagement.Services.Interfaces;

namespace UserManagement.Services.Services
{
    public class UserService : IUserService
    {
        private readonly UserManagementDbContext _context;
        private readonly IMapper _mapper;

        public UserService(UserManagementDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<GetUserDto>>> AddUser(AddUserDto newUser)
        {
            var response = new ServiceResponse<List<GetUserDto>>();
            var user = _mapper.Map<User>(newUser);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            response.Data = await _context.Users.Select(u => _mapper.Map<GetUserDto>(u)).ToListAsync();
            return response;
        }
    }
}
