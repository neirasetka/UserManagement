using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
        public async Task<ServiceResponse<List<GetExpenseDto>>> DeleteExpense(int id)
        {
            var response = new ServiceResponse<List<GetExpenseDto>>();
            try
            {

                Expense expense = await _context.Expenses.FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);
                if (expense == null)
                {
                    response.Success = false;
                    response.Message = "Expense not found!";
                    return response;
                }
                expense.IsDeleted = true;

                await _context.SaveChangesAsync();

                response.Data = _context.Expenses
                                   .Select(u => _mapper.Map<GetExpenseDto>(u)).ToList();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
