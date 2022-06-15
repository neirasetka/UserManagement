using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserManagement.Core.DTOs;

using UserManagement.Services.Interfaces;

namespace UserManagement.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;
        
        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllExpenses(int? pageNumber, int? pageSize, string? sortParametar, string? searchQuery)
        {
            var response = await _expenseService.GetAllExpenses(pageNumber, pageSize, sortParametar, searchQuery);
            if(response == null)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

  
        
        [HttpPost]
        public async Task<IActionResult> AddExpense(AddExpenseDto newExpense)
        {
            var response = await _expenseService.AddExpense(newExpense);
             if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
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
