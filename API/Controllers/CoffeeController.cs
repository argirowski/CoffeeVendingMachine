using Application.DTOs;
using Application.Features.Commands.AddCoffee;
using Application.Features.Commands.DeleteCoffee;
using Application.Features.Commands.UpdateCoffee;
using Application.Features.Queries.GetAllCoffees;
using Application.Features.Queries.GetSingleCoffee;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoffeeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CoffeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CoffeeTypeDTO>>> GetAllCoffees()
        {
            var query = new GetAllCoffeesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CoffeeTypeDTO>> GetCoffeeById(Guid id)
        {
            var query = new GetCoffeeByIdQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddCoffee([FromBody] AddCoffeeCommand command)
        {
            var coffeeTypeId = await _mediator.Send(command);
            return Ok(coffeeTypeId);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CoffeeTypeDTO>> UpdateCoffee(Guid id, [FromBody] UpdateCoffeeCommand command)
        {
            command.Id = id; // Ensure the command has the correct ID
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoffee(Guid id)
        {
            var command = new DeleteCoffeeCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
