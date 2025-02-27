using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CoffeeVendingMachineUnitTests.RepositoryTests
{
    public class DatabaseFixture : IDisposable
    {
        public ApplicationDbContext Context { get; private set; }

        public DatabaseFixture()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "CoffeeVendingMachine")
                .Options;

            Context = new ApplicationDbContext(options);
        }

        public void ClearDatabase()
        {
            Context.Database.EnsureDeleted();
            Context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}