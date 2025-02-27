using Application.DTOs;
using MediatR;

namespace Application.Features.Queries.GetSingleCoffee
{
    public class GetCoffeeByIdQuery : IRequest<CoffeeTypeDTO>
    {
        public Guid Id { get; set; }

        public GetCoffeeByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
