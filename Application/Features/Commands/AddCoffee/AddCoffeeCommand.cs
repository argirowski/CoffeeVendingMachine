using Application.DTOs;
using MediatR;

namespace Application.Features.Commands.AddCoffee
{
    public class AddCoffeeCommand : IRequest<Guid>
    {
        public required string Name { get; set; }
        public required CoffeeIngredientDTO CoffeeIngredient { get; set; }
    }
}