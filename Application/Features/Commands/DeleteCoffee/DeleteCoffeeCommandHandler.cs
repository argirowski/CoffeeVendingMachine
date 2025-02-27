using Domain.Interfaces;
using MediatR;

namespace Application.Features.Commands.DeleteCoffee
{
    public class DeleteCoffeeCommandHandler : IRequestHandler<DeleteCoffeeCommand, Unit>
    {
        private readonly ICoffeeRepository _coffeeRepository;

        public DeleteCoffeeCommandHandler(ICoffeeRepository coffeeRepository)
        {
            _coffeeRepository = coffeeRepository;
        }

        public async Task<Unit> Handle(DeleteCoffeeCommand request, CancellationToken cancellationToken)
        {
            await _coffeeRepository.DeleteCoffeeByIdAsync(request.Id);
            return Unit.Value;
        }
    }
}