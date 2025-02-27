using Application.DTOs;
using Application.Features.Commands.UpdateCoffee;
using Application.Validators;
using Domain.Entities;
using Domain.Interfaces;
using FluentValidation.TestHelper;
using Moq;

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
        public void Should_Have_Error_When_Id_Is_Empty()
        {
            // Arrange
            var coffeeTypeDTO = new CoffeeTypeDTO { Id = Guid.Empty, Name = "Espresso", CoffeeIngredient = new CoffeeIngredientDTO() };
            var command = new UpdateCoffeeCommand(coffeeTypeDTO);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.CoffeeType.Id)
                  .WithErrorMessage("Id must not be empty.");
        }

        [Fact]
        public void Should_Have_Error_When_CoffeeType_Does_Not_Exist()
        {
            // Arrange
            var coffeeId = Guid.NewGuid();
            _coffeeRepositoryMock.Setup(repo => repo.GetCoffeeByIdAsync(coffeeId))
                                 .ReturnsAsync((CoffeeType)null);
            var coffeeTypeDTO = new CoffeeTypeDTO { Id = coffeeId, Name = "Espresso", CoffeeIngredient = new CoffeeIngredientDTO() };
            var command = new UpdateCoffeeCommand(coffeeTypeDTO);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.CoffeeType.Id)
                  .WithErrorMessage("The specified coffee type does not exist.");
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Empty()
        {
            // Arrange
            var coffeeTypeDTO = new CoffeeTypeDTO { Id = Guid.NewGuid(), Name = string.Empty, CoffeeIngredient = new CoffeeIngredientDTO() };
            var command = new UpdateCoffeeCommand(coffeeTypeDTO);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.CoffeeType.Name)
                  .WithErrorMessage("Coffee name is required.");
        }

        [Fact]
        public void Should_Have_Error_When_DosesOfMilk_Is_Out_Of_Range()
        {
            // Arrange
            var coffeeTypeDTO = new CoffeeTypeDTO
            {
                Id = Guid.NewGuid(),
                Name = "Espresso",
                CoffeeIngredient = new CoffeeIngredientDTO { DosesOfMilk = 6 }
            };
            var command = new UpdateCoffeeCommand(coffeeTypeDTO);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.CoffeeType.CoffeeIngredient.DosesOfMilk)
                  .WithErrorMessage("DosesOfMilk must be between 0 and 5.");
        }

        [Fact]
        public void Should_Have_Error_When_PacksOfSugar_Is_Out_Of_Range()
        {
            // Arrange
            var coffeeTypeDTO = new CoffeeTypeDTO
            {
                Id = Guid.NewGuid(),
                Name = "Espresso",
                CoffeeIngredient = new CoffeeIngredientDTO { PacksOfSugar = 6 }
            };
            var command = new UpdateCoffeeCommand(coffeeTypeDTO);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.CoffeeType.CoffeeIngredient.PacksOfSugar)
                  .WithErrorMessage("PacksOfSugar must be between 0 and 5.");
        }
        [Fact]
        public void Should_Not_Have_Error_When_Valid()
        {
            // Arrange
            var coffeeId = Guid.NewGuid();
            var coffeeTypeDTO = new CoffeeTypeDTO
            {
                Id = coffeeId,
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
            var command = new UpdateCoffeeCommand(coffeeTypeDTO);

            _coffeeRepositoryMock.Setup(repo => repo.GetCoffeeByIdAsync(coffeeId))
                                 .ReturnsAsync(new CoffeeType { Id = coffeeId });

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}