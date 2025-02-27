using Application.DTOs;
using MediatR;

namespace Application.Features.Commands.UpdateCoffee
{
    public class UpdateCoffeeCommand : IRequest<CoffeeTypeDTO>
    {
        public CoffeeTypeDTO CoffeeType { get; set; }

        public UpdateCoffeeCommand(CoffeeTypeDTO coffeeType)
        {
            CoffeeType = coffeeType;
        }
    }
}
