using Application.DTOs;
using Application.Features.Commands.UpdateCoffee;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Moq;

namespace CoffeeVendingMachineUnitTests.HandlerTests
{
    public class UpdateCoffeeCommandHandlerTests
    {
        private readonly Mock<ICoffeeRepository> _coffeeRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly UpdateCoffeeCommandHandler _handler;

        public UpdateCoffeeCommandHandlerTests()
        {
            _coffeeRepositoryMock = new Mock<ICoffeeRepository>();
            _mapperMock = new Mock<IMapper>();
            _handler = new UpdateCoffeeCommandHandler(_coffeeRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_UpdatesCoffeeType_WhenCoffeeTypeExists()
        {
            // Arrange
            var coffeeId = Guid.NewGuid();
            var coffeeType = new CoffeeType { Id = coffeeId, Name = "Espresso", CoffeeIngredient = new CoffeeIngredient() };
            var coffeeTypeDTO = new CoffeeTypeDTO { Id = coffeeId, Name = "Espresso", CoffeeIngredient = new CoffeeIngredientDTO() };
            var command = new UpdateCoffeeCommand(coffeeTypeDTO);

            _coffeeRepositoryMock.Setup(repo => repo.GetCoffeeByIdAsync(coffeeId))
                                 .ReturnsAsync(coffeeType);
            _mapperMock.Setup(mapper => mapper.Map(command.CoffeeType, coffeeType));
            _mapperMock.Setup(mapper => mapper.Map(command.CoffeeType.CoffeeIngredient, coffeeType.CoffeeIngredient));
            _coffeeRepositoryMock.Setup(repo => repo.UpdateCoffeeAsync(coffeeType))
                                 .Returns(Task.CompletedTask);
            _mapperMock.Setup(mapper => mapper.Map<CoffeeTypeDTO>(coffeeType))
                       .Returns(coffeeTypeDTO);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(coffeeId, result.Id);
            Assert.Equal("Espresso", result.Name);
            _coffeeRepositoryMock.Verify(repo => repo.GetCoffeeByIdAsync(coffeeId), Times.Once);
            _coffeeRepositoryMock.Verify(repo => repo.UpdateCoffeeAsync(coffeeType), Times.Once);
        }
    }
}