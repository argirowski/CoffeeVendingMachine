using Application.DTOs;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Queries.GetSingleCoffee
{
    public class GetCoffeeByIdQueryHandler : IRequestHandler<GetCoffeeByIdQuery, CoffeeTypeDTO>
    {
        private readonly ICoffeeRepository _coffeeRepository;
        private readonly IMapper _mapper;

        public GetCoffeeByIdQueryHandler(ICoffeeRepository coffeeRepository, IMapper mapper)
        {
            _coffeeRepository = coffeeRepository;
            _mapper = mapper;
        }

        public async Task<CoffeeTypeDTO> Handle(GetCoffeeByIdQuery request, CancellationToken cancellationToken)
        {
            var coffeeType = await _coffeeRepository.GetCoffeeByIdAsync(request.Id);
            return _mapper.Map<CoffeeTypeDTO>(coffeeType);
        }
    }
}
