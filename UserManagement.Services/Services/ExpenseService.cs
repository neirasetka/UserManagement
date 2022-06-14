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
    public class ExpenseService : IExpenseService
    {
        private readonly UserManagementDbContext _context;
        private readonly IMapper _mapper;

        public ExpenseService(UserManagementDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<GetExpenseDto>>> GetAllExpenses(int? pageNumber, int? pageSize, string? sortParametar, string? searchQuery)
        {
            var response = new ServiceResponse<List<GetExpenseDto>>();

            var dbExpenses = await _context.Expenses.Where(v => v.IsDeleted == false).ToListAsync();

            if(searchQuery != null)
                dbExpenses = dbExpenses.Where(u=> u.Name.ToLower().Contains(searchQuery.ToLower()) 
                                          || u.Price.ToString().Contains(searchQuery.ToLower())).ToList();

            if (sortParametar != null)
            {
                switch (sortParametar)
                {
                    case "name":
                        dbExpenses = dbExpenses.OrderBy(q => q.Name).ToList();
                        break;
                    case "price":
                        dbExpenses = dbExpenses.OrderBy(q => q.Price).ToList();
                        break;
                    default:  
                        break;
                }
            }
            
            var currentPageNumber = pageNumber ?? 1;
            var currentPageSize = pageSize ?? 10;
            response.Data = dbExpenses.Skip((currentPageNumber-1)*currentPageSize).Take(currentPageSize).Select(c => _mapper.Map<GetExpenseDto>(c)).ToList();
            return response;
        }      
    }
}
