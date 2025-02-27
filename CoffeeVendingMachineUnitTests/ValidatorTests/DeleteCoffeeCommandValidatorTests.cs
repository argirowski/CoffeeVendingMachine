using Application.Features.Commands.DeleteCoffee;
using Application.Validators;
using Domain.Entities;
using Domain.Interfaces;
using FluentValidation.TestHelper;
using Moq;

namespace CoffeeVendingMachineUnitTests.ValidatorTests
{
    public class DeleteCoffeeCommandValidatorTests
    {
        private readonly DeleteCoffeeCommandValidator _validator;
        private readonly Mock<ICoffeeRepository> _coffeeRepositoryMock;

        public DeleteCoffeeCommandValidatorTests()
        {
            _coffeeRepositoryMock = new Mock<ICoffeeRepository>();
            _validator = new DeleteCoffeeCommandValidator(_coffeeRepositoryMock.Object);
        }

        [Fact]
        public void Should_Have_Error_When_Id_Is_Empty()
        {
            // Arrange
            var command = new DeleteCoffeeCommand(Guid.Empty);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Id)
                  .WithErrorMessage("Id must not be empty.");
        }

        [Fact]
        public void Should_Have_Error_When_CoffeeType_Does_Not_Exist()
        {
            // Arrange
            var coffeeId = Guid.NewGuid();
            _coffeeRepositoryMock.Setup(repo => repo.GetCoffeeByIdAsync(coffeeId))
                                 .ReturnsAsync((CoffeeType)null);
            var command = new DeleteCoffeeCommand(coffeeId);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Id)
                  .WithErrorMessage("The specified coffee type does not exist.");
        }

        [Fact]
        public void Should_Not_Have_Error_When_Valid()
        {
            // Arrange
            var coffeeId = Guid.NewGuid();
            _coffeeRepositoryMock.Setup(repo => repo.GetCoffeeByIdAsync(coffeeId))
                                 .ReturnsAsync(new CoffeeType());
            var command = new DeleteCoffeeCommand(coffeeId);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}