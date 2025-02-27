using Application.DTOs;
using AutoMapper;
using Domain.Entities;
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
            var coffeeType = await _coffeeRepository.GetCoffeeByIdAsync(request.CoffeeType.Id);


            // Map the updated values to the existing CoffeeType and CoffeeIngredient
            _mapper.Map(request.CoffeeType, coffeeType);
            _mapper.Map(request.CoffeeType.CoffeeIngredient, coffeeType.CoffeeIngredient);

            await _coffeeRepository.UpdateCoffeeAsync(coffeeType);
            return _mapper.Map<CoffeeTypeDTO>(coffeeType);
        }
    }
}
