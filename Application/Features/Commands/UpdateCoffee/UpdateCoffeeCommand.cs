using Application.DTOs;
using MediatR;

namespace Application.Features.Commands.UpdateCoffee
{
    public class UpdateCoffeeCommand : IRequest<CoffeeTypeDTO>
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required CoffeeIngredientDTO CoffeeIngredient { get; set; }
    }
}
