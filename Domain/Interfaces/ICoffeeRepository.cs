using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ICoffeeRepository
    {
        Task<IEnumerable<CoffeeType>> GetAllCoffeesAsync();
        Task<CoffeeType?> GetCoffeeByIdAsync(Guid id);
        Task AddCoffeeAsync(CoffeeType coffeeType);
        Task UpdateCoffeeAsync(CoffeeType coffeeType);
        Task DeleteCoffeeByIdAsync(Guid id);
    }
}
