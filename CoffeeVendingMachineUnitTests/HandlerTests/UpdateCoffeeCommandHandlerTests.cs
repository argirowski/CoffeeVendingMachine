using Application.DTOs;
using Application.Features.Commands.UpdateCoffee;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Moq;
using Xunit;

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
        public async Task Handle_UpdatesCoffeeAndReturnsDto()
        {
            // Arrange
            var coffeeId = Guid.NewGuid();
            var command = new UpdateCoffeeCommand
            {
                Id = coffeeId,
                Name = "Updated Espresso",
                CoffeeIngredient = new CoffeeIngredientDTO
                {
                    DosesOfMilk = 2,
                    PacksOfSugar = 1,
                    Cinnamon = true,
                    Stevia = false,
                    CoconutMilk = false
                }
            };

            var coffeeEntity = new CoffeeType
            {
                Id = coffeeId,
                Name = "Old Espresso",
                CoffeeIngredient = new CoffeeIngredient
                {
                    DosesOfMilk = 1,
                    PacksOfSugar = 2,
                    Cinnamon = false,
                    Stevia = true,
                    CoconutMilk = true
                }
            };

            var expectedDto = new CoffeeTypeDTO
            {
                Id = coffeeId,
                Name = "Updated Espresso",
                CoffeeIngredient = command.CoffeeIngredient
            };

            _coffeeRepositoryMock.Setup(r => r.GetCoffeeByIdAsync(coffeeId))
                .ReturnsAsync(coffeeEntity);

            _mapperMock.Setup(m => m.Map<CoffeeTypeDTO>(It.IsAny<CoffeeType>()))
                .Returns(expectedDto);

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            _coffeeRepositoryMock.Verify(r => r.UpdateCoffeeAsync(It.Is<CoffeeType>(c =>
                c.Name == "Updated Espresso" &&
                c.CoffeeIngredient.DosesOfMilk == 2 &&
                c.CoffeeIngredient.PacksOfSugar == 1 &&
                c.CoffeeIngredient.Cinnamon == true &&
                c.CoffeeIngredient.Stevia == false &&
                c.CoffeeIngredient.CoconutMilk == false
            )), Times.Once);

            Assert.Equal(expectedDto.Id, result.Id);
            Assert.Equal(expectedDto.Name, result.Name);
            Assert.Equal(expectedDto.CoffeeIngredient.DosesOfMilk, result.CoffeeIngredient.DosesOfMilk);
        }
    }
}