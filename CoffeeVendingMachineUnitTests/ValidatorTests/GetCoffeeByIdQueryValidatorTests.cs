using Application.Features.Queries.GetSingleCoffee;
using Application.Validators;
using Domain.Entities;
using Domain.Interfaces;
using FluentValidation.TestHelper;
using Moq;

namespace CoffeeVendingMachineUnitTests.ValidatorTests
{
    public class GetCoffeeByIdQueryValidatorTests
    {
        private readonly GetCoffeeByIdQueryValidator _validator;
        private readonly Mock<ICoffeeRepository> _coffeeRepositoryMock;

        public GetCoffeeByIdQueryValidatorTests()
        {
            _coffeeRepositoryMock = new Mock<ICoffeeRepository>();
            _validator = new GetCoffeeByIdQueryValidator(_coffeeRepositoryMock.Object);
        }

        [Fact]
        public void Should_Have_Error_When_Id_Is_Empty()
        {
            // Arrange
            var query = new GetCoffeeByIdQuery(Guid.Empty);

            // Act
            var result = _validator.TestValidate(query);

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
            var query = new GetCoffeeByIdQuery(coffeeId);

            // Act
            var result = _validator.TestValidate(query);

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
                                 .ReturnsAsync(new CoffeeType
                                 {
                                     Id = coffeeId,
                                     Name = "Espresso"
                                 });
            var query = new GetCoffeeByIdQuery(coffeeId);

            // Act
            var result = _validator.TestValidate(query);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}