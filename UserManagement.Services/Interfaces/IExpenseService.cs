using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Core.DTOs;
using UserManagement.Core.Entities;

namespace UserManagement.Services.Interfaces
{
    public interface IExpenseService
    {
        Task<ServiceResponse<List<GetExpenseDto>>> AddExpense(AddExpenseDto newExpense);

    }
}