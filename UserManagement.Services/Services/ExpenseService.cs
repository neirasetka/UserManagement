using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Core.DTOs.ExpenseDto;
using UserManagement.Core.Entities;
using UserManagement.Database;
using UserManagement.Services.Interfaces;

namespace UserManagement.Services.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly UserManagementDbContext _context;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public ExpenseService(UserManagementDbContext context, IMapper mapper, IEmailService emailService)
        {
            _context = context;
            _mapper = mapper;
            _emailService = emailService;
        }
        public async Task<ServiceResponse<List<GetExpenseDto>>> GetAllExpenses(int? pageNumber, int? pageSize, string? sortParametar, string? searchQuery)
        {
            var response = new ServiceResponse<List<GetExpenseDto>>();

            var dbExpenses = await _context.Expenses.Where(v => v.IsDeleted == false).ToListAsync();

            if (searchQuery != null)
                dbExpenses = dbExpenses.Where(u => u.Name.ToLower().Contains(searchQuery.ToLower())
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
            response.Data = dbExpenses.Skip((currentPageNumber - 1) * currentPageSize).Take(currentPageSize).Select(c => _mapper.Map<GetExpenseDto>(c)).ToList();
            return response;
        }

        public async Task<ServiceResponse<List<GetExpenseDto>>> AddExpense(AddExpenseDto newExpense)
        {
            var response = new ServiceResponse<List<GetExpenseDto>>();
            var vehicle = _context.Vehicles.Include(v => v.User).FirstOrDefault(v => v.Id == newExpense.VehicleId);
            var expense = _mapper.Map<Expense>(newExpense);
            expense.Vehicle = vehicle;
            var userEmail = expense.Vehicle.User.Email;
            var userName = expense.Vehicle.User.FirstName + expense.Vehicle.User.LastName;
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();
            response.Data = await _context.Expenses.Select(u => _mapper.Map<GetExpenseDto>(u)).ToListAsync();
            if (expense.ExpirationDate > DateTime.Now && expense.ExpirationDate > DateTime.Now.AddDays(10))
                await _emailService.SendEmail(userEmail, userName, expense.Name, expense.ExpirationDate);
            return response;
        }
        public async Task<ServiceResponse<List<GetExpenseDto>>> DeleteExpense(int id)
        {
            var response = new ServiceResponse<List<GetExpenseDto>>();
            try
            {

                Expense expense = await _context.Expenses.FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
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

        public async Task<ServiceResponse<GetExpenseDto>> GetExpenseById(int id)
        {
            var response = new ServiceResponse<GetExpenseDto>();
            try
            {
                Expense expense = await _context.Expenses
                    .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
                if (expense == null)
                {
                    response.Success = false;
                    response.Message = "Expense not found!";
                    return response;
                }
                response.Data = _mapper.Map<GetExpenseDto>(expense);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<GetExpenseDto>> UpdateExpense(UpdateExpenseDto updatedExpense)
        {
            var response = new ServiceResponse<GetExpenseDto>();
            try
            {
                Expense expense = await _context.Expenses
                    .FirstOrDefaultAsync(c => c.Id == updatedExpense.Id && !c.IsDeleted);
                var vehicle = _context.Vehicles.Include(v => v.User).FirstOrDefault(v => v.Id == updatedExpense.VehicleId);
                if (expense == null)
                {
                    response.Success = false;
                    response.Message = "Expense not found!";
                    return response;
                }
                expense.Vehicle = vehicle;
                expense.Name = updatedExpense.Name;
                expense.Price = updatedExpense.Price;
                expense.IsDeleted = updatedExpense.IsDeleted;
                var userEmail = expense.Vehicle.User.Email;
                var userName = expense.Vehicle.User.FirstName + expense.Vehicle.User.LastName;
                if (expense.ExpirationDate != updatedExpense.ExpirationDate && updatedExpense.ExpirationDate > DateTime.Now.AddDays(10))
                    await _emailService.SendEmail(userEmail, userName, updatedExpense.Name, updatedExpense.ExpirationDate);
                expense.ExpirationDate = updatedExpense.ExpirationDate;
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetExpenseDto>(expense);
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