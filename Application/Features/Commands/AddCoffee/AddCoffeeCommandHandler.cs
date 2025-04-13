using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Commands.AddCoffee
{
    public class AddCoffeeCommandHandler : IRequestHandler<AddCoffeeCommand, Guid>
    {
        private readonly ICoffeeRepository _coffeeRepository;

        public AddCoffeeCommandHandler(ICoffeeRepository coffeeRepository)
        {
            _coffeeRepository = coffeeRepository;
        }

        public async Task<Guid> Handle(AddCoffeeCommand request, CancellationToken cancellationToken)
        {
            var coffeeType = new CoffeeType
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                CoffeeIngredient = new CoffeeIngredient
                {
                    Id = Guid.NewGuid(),
                    DosesOfMilk = request.CoffeeIngredient.DosesOfMilk,
                    PacksOfSugar = request.CoffeeIngredient.PacksOfSugar,
                    Cinnamon = request.CoffeeIngredient.Cinnamon,
                    Stevia = request.CoffeeIngredient.Stevia,
                    CoconutMilk = request.CoffeeIngredient.CoconutMilk
                }
            };

            await _coffeeRepository.AddCoffeeAsync(coffeeType);
            return coffeeType.Id;
        }
    }
}