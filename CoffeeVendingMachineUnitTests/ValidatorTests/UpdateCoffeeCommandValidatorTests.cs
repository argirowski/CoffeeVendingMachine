using Application.DTOs;
using Application.Features.Commands.UpdateCoffee;
using Application.Validators;
using Domain.Entities;
using Domain.Interfaces;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace CoffeeVendingMachineUnitTests.ValidatorTests
{
    public class UpdateCoffeeCommandValidatorTests
    {
        private readonly UpdateCoffeeCommandValidator _validator;
        private readonly Mock<ICoffeeRepository> _coffeeRepositoryMock;

        public UpdateCoffeeCommandValidatorTests()
        {
            _coffeeRepositoryMock = new Mock<ICoffeeRepository>();
            _validator = new UpdateCoffeeCommandValidator(_coffeeRepositoryMock.Object);
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Empty()
        {
            var command = new UpdateCoffeeCommand
            {
                Id = Guid.NewGuid(),
                Name = "",
                CoffeeIngredient = new CoffeeIngredientDTO()
            };

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Name)
                .WithErrorMessage("Coffee name is required.");
        }

        [Fact]
        public void Should_Have_Error_When_Id_Is_Empty()
        {
            var command = new UpdateCoffeeCommand
            {
                Id = Guid.Empty,
                Name = "Espresso",
                CoffeeIngredient = new CoffeeIngredientDTO()
            };

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Id);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Valid()
        {
            var command = new UpdateCoffeeCommand
            {
                Id = Guid.NewGuid(),
                Name = "Espresso",
                CoffeeIngredient = new CoffeeIngredientDTO
                {
                    DosesOfMilk = 1,
                    PacksOfSugar = 2,
                    Cinnamon = true,
                    Stevia = false,
                    CoconutMilk = true
                }
            };

            _coffeeRepositoryMock.Setup(r => r.GetCoffeeByIdAsync(command.Id))
                .ReturnsAsync(new CoffeeType { Id = command.Id, Name = command.Name });

            var result = _validator.TestValidate(command);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}