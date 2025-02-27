using Application.DTOs;
using MediatR;

namespace Application.Features.Queries.GetAllCoffees
{
    public class GetAllCoffeesQuery : IRequest<IEnumerable<CoffeeTypeDTO>>
    {
    }
}
