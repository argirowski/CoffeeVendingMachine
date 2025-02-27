using Application.DTOs;
using FluentValidation;

namespace Application.Validators
{
    public class BaseCoffeeIngredientValidator : AbstractValidator<CoffeeIngredientDTO>
    {
        public BaseCoffeeIngredientValidator()
        {
            RuleFor(x => x.DosesOfMilk)
                .NotNull().WithMessage("DosesOfMilk is required.")
                .InclusiveBetween(0, 5).WithMessage("DosesOfMilk must be between 0 and 5.");

            RuleFor(x => x.PacksOfSugar)
                .NotNull().WithMessage("PacksOfSugar is required.")
                .InclusiveBetween(0, 5).WithMessage("PacksOfSugar must be between 0 and 5.");

            RuleFor(x => x.Cinnamon)
                .NotNull().WithMessage("Cinnamon is required.");

            RuleFor(x => x.Stevia)
                .NotNull().WithMessage("Stevia is required.");

            RuleFor(x => x.CoconutMilk)
                .NotNull().WithMessage("CoconutMilk is required.");
        }
    }
}