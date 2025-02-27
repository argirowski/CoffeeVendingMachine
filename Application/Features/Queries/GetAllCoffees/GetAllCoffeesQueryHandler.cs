using Application.DTOs;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Queries.GetAllCoffees
{
    public class GetAllCoffeesQueryHandler : IRequestHandler<GetAllCoffeesQuery, IEnumerable<CoffeeTypeDTO>>
    {
        private readonly ICoffeeRepository _coffeeRepository;
        private readonly IMapper _mapper;

        public GetAllCoffeesQueryHandler(ICoffeeRepository coffeeRepository, IMapper mapper)
        {
            _coffeeRepository = coffeeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CoffeeTypeDTO>> Handle(GetAllCoffeesQuery request, CancellationToken cancellationToken)
        {
            var coffeeTypes = await _coffeeRepository.GetAllCoffeesAsync();
            return _mapper.Map<IEnumerable<CoffeeTypeDTO>>(coffeeTypes);
        }
    }
}
