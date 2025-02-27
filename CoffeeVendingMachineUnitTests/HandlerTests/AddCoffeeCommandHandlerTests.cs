using Application.DTOs;
using Application.Features.Commands.AddCoffee;
using Domain.Entities;
using Domain.Interfaces;
using Moq;

namespace CoffeeVendingMachineUnitTests.HandlerTests
{
    public class AddCoffeeCommandHandlerTests
    {
        private readonly Mock<ICoffeeRepository> _coffeeRepositoryMock;
        private readonly AddCoffeeCommandHandler _handler;

        public AddCoffeeCommandHandlerTests()
        {
            _coffeeRepositoryMock = new Mock<ICoffeeRepository>();
            _handler = new AddCoffeeCommandHandler(_coffeeRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ReturnsNewCoffeeTypeId_WhenCoffeeTypeIsAdded()
        {
            // Arrange
            var coffeeId = Guid.NewGuid();
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

            _coffeeRepositoryMock.Setup(repo => repo.AddCoffeeAsync(It.IsAny<CoffeeType>()))
                                 .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotEqual(Guid.Empty, result);
            _coffeeRepositoryMock.Verify(repo => repo.AddCoffeeAsync(It.IsAny<CoffeeType>()), Times.Once);
        }
    }
}