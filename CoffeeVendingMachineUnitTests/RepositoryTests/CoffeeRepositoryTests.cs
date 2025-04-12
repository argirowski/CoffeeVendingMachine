using Domain.Entities;
using Persistence.Repositories;
using Persistence;

namespace CoffeeVendingMachineUnitTests.RepositoryTests
{
    public class CoffeeRepositoryTests : IClassFixture<DatabaseFixture>
    {
        private readonly ApplicationDbContext _context;
        private readonly CoffeeRepository _repository;
        private readonly DatabaseFixture _fixture;

        public CoffeeRepositoryTests(DatabaseFixture fixture)
        {
            _fixture = fixture;
            _context = fixture.Context;
            _repository = new CoffeeRepository(_context);
        }

        [Fact]
        public async Task GetAllCoffeesAsync_ReturnsAllCoffees()
        {
            // Arrange
            _fixture.ClearDatabase();
            var coffeeTypes = new List<CoffeeType>
            {
                new CoffeeType { Id = Guid.NewGuid(), Name = "Espresso", CoffeeIngredient = new CoffeeIngredient() },
                new CoffeeType { Id = Guid.NewGuid(), Name = "Latte", CoffeeIngredient = new CoffeeIngredient() }
            };
            _context.CoffeeTypes.AddRange(coffeeTypes);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetAllCoffeesAsync();

            // Assert
            Assert.Equal(5, result.Count());
            Assert.Contains(result, ct => ct.Name == "Espresso");
            Assert.Contains(result, ct => ct.Name == "Latte");
        }

        [Fact]
        public async Task GetCoffeeByIdAsync_ReturnsCoffeeType_WhenCoffeeTypeExists()
        {
            // Arrange
            _fixture.ClearDatabase();
            var coffeeId = Guid.NewGuid();
            var coffeeType = new CoffeeType { Id = coffeeId, Name = "Espresso", CoffeeIngredient = new CoffeeIngredient() };
            _context.CoffeeTypes.Add(coffeeType);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetCoffeeByIdAsync(coffeeId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(coffeeId, result.Id);
        }

        [Fact]
        public async Task GetCoffeeByIdAsync_ReturnsNull_WhenCoffeeTypeDoesNotExist()
        {
            // Arrange
            _fixture.ClearDatabase();
            var coffeeId = Guid.NewGuid();

            // Act
            var result = await _repository.GetCoffeeByIdAsync(coffeeId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddCoffeeAsync_AddsCoffeeType()
        {
            // Arrange
            _fixture.ClearDatabase();
            var coffeeType = new CoffeeType { Id = Guid.NewGuid(), Name = "Espresso", CoffeeIngredient = new CoffeeIngredient() };

            // Act
            await _repository.AddCoffeeAsync(coffeeType);
            var result = await _context.CoffeeTypes.FindAsync(coffeeType.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(coffeeType.Id, result.Id);
        }

        [Fact]
        public async Task UpdateCoffeeAsync_UpdatesCoffeeType()
        {
            // Arrange
            _fixture.ClearDatabase();
            var coffeeType = new CoffeeType { Id = Guid.NewGuid(), Name = "Espresso", CoffeeIngredient = new CoffeeIngredient() };
            _context.CoffeeTypes.Add(coffeeType);
            await _context.SaveChangesAsync();

            coffeeType.Name = "Updated Espresso";

            // Act
            await _repository.UpdateCoffeeAsync(coffeeType);
            var result = await _context.CoffeeTypes.FindAsync(coffeeType.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Updated Espresso", result.Name);
        }

        [Fact]
        public async Task DeleteCoffeeByIdAsync_DeletesCoffeeType()
        {
            // Arrange
            _fixture.ClearDatabase();
            var coffeeId = Guid.NewGuid();
            var coffeeType = new CoffeeType { Id = coffeeId, Name = "Espresso", CoffeeIngredient = new CoffeeIngredient() };
            _context.CoffeeTypes.Add(coffeeType);
            await _context.SaveChangesAsync();

            // Act
            await _repository.DeleteCoffeeByIdAsync(coffeeId);
            var result = await _context.CoffeeTypes.FindAsync(coffeeId);

            // Assert
            Assert.Null(result);
        }
    }
}