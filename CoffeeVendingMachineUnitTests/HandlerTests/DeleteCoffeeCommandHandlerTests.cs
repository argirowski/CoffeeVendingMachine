using Application.Features.Commands.DeleteCoffee;
using Domain.Interfaces;
using MediatR;
using Moq;

namespace CoffeeVendingMachineUnitTests.HandlerTests
{
    public class DeleteCoffeeCommandHandlerTests
    {
        private readonly Mock<ICoffeeRepository> _coffeeRepositoryMock;
        private readonly DeleteCoffeeCommandHandler _handler;

        public DeleteCoffeeCommandHandlerTests()
        {
            _coffeeRepositoryMock = new Mock<ICoffeeRepository>();
            _handler = new DeleteCoffeeCommandHandler(_coffeeRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_DeletesCoffeeType_WhenCoffeeTypeExists()
        {
            // Arrange
            var coffeeId = Guid.NewGuid();
            var command = new DeleteCoffeeCommand(coffeeId);

            _coffeeRepositoryMock.Setup(repo => repo.DeleteCoffeeByIdAsync(coffeeId))
                                 .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(Unit.Value, result);
            _coffeeRepositoryMock.Verify(repo => repo.DeleteCoffeeByIdAsync(coffeeId), Times.Once);
        }
    }
}