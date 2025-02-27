using Application.DTOs;
using Application.Features.Queries.GetSingleCoffee;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Moq;

namespace CoffeeVendingMachineUnitTests.HandlerTests
{
    public class GetCoffeeByIdQueryHandlerTests
    {
        private readonly Mock<ICoffeeRepository> _coffeeRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly GetCoffeeByIdQueryHandler _handler;

        public GetCoffeeByIdQueryHandlerTests()
        {
            _coffeeRepositoryMock = new Mock<ICoffeeRepository>();
            _mapperMock = new Mock<IMapper>();
            _handler = new GetCoffeeByIdQueryHandler(_coffeeRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_ReturnsCoffeeTypeDTO_WhenCoffeeTypeExists()
        {
            // Arrange
            var coffeeId = Guid.NewGuid();
            var coffeeType = new CoffeeType { Id = coffeeId, Name = "Espresso", CoffeeIngredient = new CoffeeIngredient() };
            var coffeeTypeDTO = new CoffeeTypeDTO { Id = coffeeId, Name = "Espresso", CoffeeIngredient = new CoffeeIngredientDTO() };

            _coffeeRepositoryMock.Setup(repo => repo.GetCoffeeByIdAsync(coffeeId))
                                 .ReturnsAsync(coffeeType);
            _mapperMock.Setup(mapper => mapper.Map<CoffeeTypeDTO>(coffeeType))
                       .Returns(coffeeTypeDTO);

            var query = new GetCoffeeByIdQuery(coffeeId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(coffeeId, result.Id);
            Assert.Equal("Espresso", result.Name);
        }

        [Fact]
        public async Task Handle_ReturnsNull_WhenCoffeeTypeDoesNotExist()
        {
            // Arrange
            var coffeeId = Guid.NewGuid();

            _coffeeRepositoryMock.Setup(repo => repo.GetCoffeeByIdAsync(coffeeId))
                                 .ReturnsAsync((CoffeeType)null);

            var query = new GetCoffeeByIdQuery(coffeeId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}