using Application.Features.Commands.DeleteCoffee;
using Domain.Interfaces;
using FluentValidation;

namespace Application.Validators
{
    public class DeleteCoffeeCommandValidator : AbstractValidator<DeleteCoffeeCommand>
    {
        public DeleteCoffeeCommandValidator(ICoffeeRepository coffeeRepository)
        {
            RuleFor(x => x.Id)
                .SetValidator(new BaseIdValidator(coffeeRepository));
        }
    }
}