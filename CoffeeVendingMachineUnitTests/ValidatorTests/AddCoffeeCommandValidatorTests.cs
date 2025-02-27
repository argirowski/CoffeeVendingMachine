using Application.DTOs;
using Application.Features.Commands.AddCoffee;
using Application.Validators;
using FluentValidation.TestHelper;

namespace CoffeeVendingMachineUnitTests.ValidatorTests
{
    public class AddCoffeeCommandValidatorTests
    {
        private readonly AddCoffeeCommandValidator _validator;

        public AddCoffeeCommandValidatorTests()
        {
            _validator = new AddCoffeeCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Null()
        {
            // Arrange
            var command = new AddCoffeeCommand { Name = null, CoffeeIngredient = new CoffeeIngredientDTO() };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name)
                  .WithErrorMessage("Name is required.");
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Empty()
        {
            // Arrange
            var command = new AddCoffeeCommand { Name = string.Empty, CoffeeIngredient = new CoffeeIngredientDTO() };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name)
                  .WithErrorMessage("Name is required.");
        }

        [Fact]
        public void Should_Have_Error_When_CoffeeIngredient_Is_Null()
        {
            // Arrange
            var command = new AddCoffeeCommand { Name = "Espresso", CoffeeIngredient = null };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.CoffeeIngredient)
                  .WithErrorMessage("CoffeeIngredient is required.");
        }

        [Fact]
        public void Should_Have_Error_When_DosesOfMilk_Is_Out_Of_Range()
        {
            // Arrange
            var command = new AddCoffeeCommand
            {
                Name = "Espresso",
                CoffeeIngredient = new CoffeeIngredientDTO { DosesOfMilk = 6 }
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.CoffeeIngredient.DosesOfMilk)
                  .WithErrorMessage("DosesOfMilk must be between 0 and 5.");
        }

        [Fact]
        public void Should_Have_Error_When_PacksOfSugar_Is_Out_Of_Range()
        {
            // Arrange
            var command = new AddCoffeeCommand
            {
                Name = "Espresso",
                CoffeeIngredient = new CoffeeIngredientDTO { PacksOfSugar = 6 }
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.CoffeeIngredient.PacksOfSugar)
                  .WithErrorMessage("PacksOfSugar must be between 0 and 5.");
        }

        [Fact]
        public void Should_Not_Have_Error_When_Valid()
        {
            // Arrange
            var command = new AddCoffeeCommand
            {
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

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}