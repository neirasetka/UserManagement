using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Core.DTOs.ExpenseDto;
using UserManagement.Core.Entities;

namespace UserManagement.Services.Interfaces
{
    public interface IExpenseService
    {

        Task<ServiceResponse<List<GetExpenseDto>>> GetAllExpenses(int? pageNumber, int? pageSize, string? sortParametar, string? searchQuery);
        Task<ServiceResponse<List<GetExpenseDto>>> AddExpense(AddExpenseDto newExpense);
        Task<ServiceResponse<List<GetExpenseDto>>> DeleteExpense(int id);
        Task<ServiceResponse<GetExpenseDto>> UpdateExpense(UpdateExpenseDto updatedExpense);
        Task<ServiceResponse<GetExpenseDto>> GetExpenseById(int id);
    }
}