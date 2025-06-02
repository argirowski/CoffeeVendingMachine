using API.Controllers;
using Application.DTOs;
using Application.Features.Commands.AddCoffee;
using Application.Features.Commands.DeleteCoffee;
using Application.Features.Commands.UpdateCoffee;
using Application.Features.Queries.GetAllCoffees;
using Application.Features.Queries.GetSingleCoffee;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CoffeeVendingMachineUnitTests.ControllerTests
{
    public class CoffeeControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly CoffeeController _controller;

        public CoffeeControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new CoffeeController(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetAllCoffees_ReturnsOkResult_WithListOfCoffees()
        {
            // Arrange
            var coffeeList = new List<CoffeeTypeDTO>
            {
                new CoffeeTypeDTO { Id = Guid.NewGuid(), Name = "Espresso", CoffeeIngredient = new CoffeeIngredientDTO() },
                new CoffeeTypeDTO { Id = Guid.NewGuid(), Name = "Latte", CoffeeIngredient = new CoffeeIngredientDTO() }
            };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllCoffeesQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(coffeeList);

            // Act
            var result = await _controller.GetAllCoffees();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<CoffeeTypeDTO>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetCoffeeById_ReturnsOkResult_WithCoffee()
        {
            // Arrange
            var coffeeId = Guid.NewGuid();
            var coffee = new CoffeeTypeDTO { Id = coffeeId, Name = "Espresso", CoffeeIngredient = new CoffeeIngredientDTO() };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetCoffeeByIdQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(coffee);

            // Act
            var result = await _controller.GetCoffeeById(coffeeId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<CoffeeTypeDTO>(okResult.Value);
            Assert.Equal(coffeeId, returnValue.Id);
        }

        [Fact]
        public async Task AddCoffee_ReturnsOkResult_WithCoffeeId()
        {
            // Arrange
            var coffeeId = Guid.NewGuid();
            var command = new AddCoffeeCommand { Name = "Espresso", CoffeeIngredient = new CoffeeIngredientDTO() };
            _mediatorMock.Setup(m => m.Send(It.IsAny<AddCoffeeCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(coffeeId);

            // Act
            var result = await _controller.AddCoffee(command);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Guid>(okResult.Value);
            Assert.Equal(coffeeId, returnValue);
        }

        [Fact]
        public async Task DeleteCoffee_ReturnsNoContentResult()
        {
            // Arrange
            var coffeeId = Guid.NewGuid();
            var command = new DeleteCoffeeCommand(coffeeId);
            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteCoffeeCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(Unit.Value);

            // Act
            var result = await _controller.DeleteCoffee(coffeeId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateCoffee_ReturnsOkResult_WithUpdatedCoffee()
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
            var updatedCoffee = new CoffeeTypeDTO
            {
                Id = coffeeId,
                Name = "Updated Espresso",
                CoffeeIngredient = command.CoffeeIngredient
            };

            _mediatorMock.Setup(m => m.Send(It.Is<UpdateCoffeeCommand>(c => c.Id == coffeeId), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(updatedCoffee);

            // Act
            var result = await _controller.UpdateCoffee(coffeeId, command);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<CoffeeTypeDTO>(okResult.Value);
            Assert.Equal(coffeeId, returnValue.Id);
            Assert.Equal("Updated Espresso", returnValue.Name);
            Assert.Equal(2, returnValue.CoffeeIngredient.DosesOfMilk);
        }
    }
}