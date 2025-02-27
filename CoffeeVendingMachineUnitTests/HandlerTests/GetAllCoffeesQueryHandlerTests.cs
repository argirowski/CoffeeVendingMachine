using Application.DTOs;
using Application.Features.Queries.GetAllCoffees;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Moq;

namespace CoffeeVendingMachineUnitTests.HandlerTests
{
    public class GetAllCoffeesQueryHandlerTests
    {
        private readonly Mock<ICoffeeRepository> _coffeeRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly GetAllCoffeesQueryHandler _handler;

        public GetAllCoffeesQueryHandlerTests()
        {
            _coffeeRepositoryMock = new Mock<ICoffeeRepository>();
            _mapperMock = new Mock<IMapper>();
            _handler = new GetAllCoffeesQueryHandler(_coffeeRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_ReturnsListOfCoffeeTypeDTOs()
        {
            // Arrange
            var coffeeTypes = new List<CoffeeType>
            {
                new CoffeeType { Id = Guid.NewGuid(), Name = "Espresso", CoffeeIngredient = new CoffeeIngredient() },
                new CoffeeType { Id = Guid.NewGuid(), Name = "Latte", CoffeeIngredient = new CoffeeIngredient() }
            };
            var coffeeTypeDTOs = new List<CoffeeTypeDTO>
            {
                new CoffeeTypeDTO { Id = coffeeTypes[0].Id, Name = "Espresso", CoffeeIngredient = new CoffeeIngredientDTO() },
                new CoffeeTypeDTO { Id = coffeeTypes[1].Id, Name = "Latte", CoffeeIngredient = new CoffeeIngredientDTO() }
            };

            _coffeeRepositoryMock.Setup(repo => repo.GetAllCoffeesAsync())
                                 .ReturnsAsync(coffeeTypes);
            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<CoffeeTypeDTO>>(coffeeTypes))
                       .Returns(coffeeTypeDTOs);

            var query = new GetAllCoffeesQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Equal("Espresso", result.First().Name);
            Assert.Equal("Latte", result.Last().Name);
        }
    }
}