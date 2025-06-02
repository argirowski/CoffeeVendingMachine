using Application.Features.Commands.UpdateCoffee;
using Domain.Interfaces;
using FluentValidation;

namespace Application.Validators
{
    public class UpdateCoffeeCommandValidator : AbstractValidator<UpdateCoffeeCommand>
    {
        private readonly ICoffeeRepository _coffeeRepository;

        public UpdateCoffeeCommandValidator(ICoffeeRepository coffeeRepository)
        {
            _coffeeRepository = coffeeRepository;

            RuleFor(x => x.Id)
                .SetValidator(new BaseIdValidator(_coffeeRepository));
            RuleFor(x => x.Name).NotEmpty().WithMessage("Coffee name is required.");
            RuleFor(x => x.CoffeeIngredient).SetValidator(new BaseCoffeeIngredientValidator());
        }
    }
}