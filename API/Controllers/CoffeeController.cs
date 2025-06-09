using Application.DTOs;
using Application.Features.Commands.AddCoffee;
using Application.Features.Commands.DeleteCoffee;
using Application.Features.Commands.UpdateCoffee;
using Application.Features.Queries.GetAllCoffees;
using Application.Features.Queries.GetSingleCoffee;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoffeeController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CoffeeController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
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
        public async Task<IActionResult> AddCoffee([FromBody] AddCoffeeDTO dto)
        {
            var command = _mapper.Map<AddCoffeeCommand>(dto);
            var coffeeTypeId = await _mediator.Send(command);
            return Ok(coffeeTypeId);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CoffeeTypeDTO>> UpdateCoffee(Guid id, [FromBody] UpdateCoffeeDTO dto)
        {
            var command = _mapper.Map<UpdateCoffeeCommand>(dto);
            command.Id = id;
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
