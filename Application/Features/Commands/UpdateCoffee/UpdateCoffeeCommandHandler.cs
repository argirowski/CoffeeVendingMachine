using Application.DTOs;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Commands.UpdateCoffee
{
    public class UpdateCoffeeCommandHandler : IRequestHandler<UpdateCoffeeCommand, CoffeeTypeDTO>
    {
        private readonly ICoffeeRepository _coffeeRepository;
        private readonly IMapper _mapper;

        public UpdateCoffeeCommandHandler(ICoffeeRepository coffeeRepository, IMapper mapper)
        {
            _coffeeRepository = coffeeRepository;
            _mapper = mapper;
        }

        public async Task<CoffeeTypeDTO> Handle(UpdateCoffeeCommand request, CancellationToken cancellationToken)
        {
            var coffeeType = await _coffeeRepository.GetCoffeeByIdAsync(request.Id);

            coffeeType.Name = request.Name;
            if (coffeeType.CoffeeIngredient != null && request.CoffeeIngredient != null)
            {
                coffeeType.CoffeeIngredient.DosesOfMilk = request.CoffeeIngredient.DosesOfMilk;
                coffeeType.CoffeeIngredient.PacksOfSugar = request.CoffeeIngredient.PacksOfSugar;
                coffeeType.CoffeeIngredient.Cinnamon = request.CoffeeIngredient.Cinnamon;
                coffeeType.CoffeeIngredient.Stevia = request.CoffeeIngredient.Stevia;
                coffeeType.CoffeeIngredient.CoconutMilk = request.CoffeeIngredient.CoconutMilk;
            }

            await _coffeeRepository.UpdateCoffeeAsync(coffeeType);
            return _mapper.Map<CoffeeTypeDTO>(coffeeType);
        }
    }
}
