using Domain.Interfaces;
using FluentValidation;

namespace Application.Validators
{
    public class BaseIdValidator : AbstractValidator<Guid>
    {
        private readonly ICoffeeRepository _coffeeRepository;

        public BaseIdValidator(ICoffeeRepository coffeeRepository)
        {
            _coffeeRepository = coffeeRepository;

            RuleFor(id => id)
                .NotEmpty().WithMessage("Id must not be empty.")
                .Must(ExistInDatabase).WithMessage("The specified coffee type does not exist.");
        }

        private bool ExistInDatabase(Guid id)
        {
            var coffeeType = _coffeeRepository.GetCoffeeByIdAsync(id).Result;
            return coffeeType != null;
        }
    }
}