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
        public async Task<ServiceResponse<List<GetExpenseDto>>> AddExpense(AddExpenseDto newExpense)
        {
            var response = new ServiceResponse<List<GetExpenseDto>>();
            var expense = _mapper.Map<Expense>(newExpense);
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();
            response.Data = await _context.Expenses.Select(u => _mapper.Map<GetExpenseDto>(u)).ToListAsync();
            return response;
        }
    }
}