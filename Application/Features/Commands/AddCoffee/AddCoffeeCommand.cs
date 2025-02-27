using Application.DTOs;
using MediatR;

namespace Application.Features.Commands.AddCoffee
{
    public class AddCoffeeCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public CoffeeIngredientDTO CoffeeIngredient { get; set; }
    }
}