using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Core.DTOs;
using UserManagement.Services.Interfaces;

namespace UserManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;
        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            var response = await _expenseService.DeleteExpense(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);

        }
        [HttpPut]
        public async Task<IActionResult> UpdateExpense(UpdateExpenseDto updatedExpense)
        {
            var response = await _expenseService.UpdateExpense(updatedExpense);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetExpenseById(int id)
        {
            var response = await _expenseService.GetExpenseById(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);

        }
    }
}
