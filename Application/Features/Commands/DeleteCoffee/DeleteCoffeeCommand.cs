using MediatR;

namespace Application.Features.Commands.DeleteCoffee
{
    public class DeleteCoffeeCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }

        public DeleteCoffeeCommand(Guid id)
        {
            Id = id;
        }
    }
}
