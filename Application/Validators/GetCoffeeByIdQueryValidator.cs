using Application.Features.Queries.GetSingleCoffee;
using Domain.Interfaces;
using FluentValidation;

namespace Application.Validators
{
    public class GetCoffeeByIdQueryValidator : AbstractValidator<GetCoffeeByIdQuery>
    {
        public GetCoffeeByIdQueryValidator(ICoffeeRepository coffeeRepository)
        {
            RuleFor(x => x.Id)
                .SetValidator(new BaseIdValidator(coffeeRepository));
        }
    }
}