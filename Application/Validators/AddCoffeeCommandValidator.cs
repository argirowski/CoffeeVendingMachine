using Application.Features.Commands.AddCoffee;
using FluentValidation;

namespace Application.Validators
{
    public class AddCoffeeCommandValidator : AbstractValidator<AddCoffeeCommand>
    {
        public AddCoffeeCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.");

            RuleFor(x => x.CoffeeIngredient)
                .NotNull().WithMessage("CoffeeIngredient is required.")
                .SetValidator(new BaseCoffeeIngredientValidator());
        }
    }
}