using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserManagement.Services.Interfaces;

namespace UserManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _service;

        public ExpenseController(IExpenseService service)
        {
            _service = service;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllExpenses(int? pageNumber, int? pageSize, string? sortParametar, string? searchQuery)
        {
            var response = await _service.GetAllExpenses(pageNumber, pageSize, sortParametar, searchQuery);
            if(response == null)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
