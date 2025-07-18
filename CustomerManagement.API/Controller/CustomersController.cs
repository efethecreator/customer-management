using CustomerManagement.Application.Customers.Commands.CreateCustomer;
using CustomerManagement.Application.Customers.Commands.UpdateCustomer;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CustomerManagement.Application.Customers.Queries.GetAllCustomers;
using CustomerManagement.Application.Customers.Commands.DeleteCustomer;

namespace CustomerManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerCommand command)
        {
            var customerId = await _mediator.Send(command);
            return Ok(new { Id = customerId });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _mediator.Send(new GetAllCustomersQuery());
            return Ok(customers);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] UpdateCustomerCommand command)
        {
            if (id != command.Id) return BadRequest("ID uyuşmuyor.");

            var updated = await _mediator.Send(command);
            if (!updated) return NotFound();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var deleted = await _mediator.Send(new DeleteCustomerCommand(id));
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}

