using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CoffeeRepository : ICoffeeRepository
    {
        private readonly ApplicationDbContext _context;

        public CoffeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CoffeeType>> GetAllCoffeesAsync()
        {
            return await _context.CoffeeTypes
                .Include(ct => ct.CoffeeIngredient)
                .OrderBy(ct => ct.Name)
                .ToListAsync();
        }

        public async Task<CoffeeType> GetCoffeeByIdAsync(Guid id)
        {
            return await _context.CoffeeTypes
                .Include(ct => ct.CoffeeIngredient)
                .FirstOrDefaultAsync(ct => ct.Id == id);
        }

        public async Task AddCoffeeAsync(CoffeeType coffeeType)
        {
            _context.CoffeeTypes.Add(coffeeType);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCoffeeAsync(CoffeeType coffeeType)
        {
            _context.CoffeeTypes.Update(coffeeType);
            _context.CoffeeIngredients.Update(coffeeType.CoffeeIngredient);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCoffeeByIdAsync(Guid id)
        {
            var coffeeType = await _context.CoffeeTypes
                .Include(ct => ct.CoffeeIngredient)
                .FirstOrDefaultAsync(ct => ct.Id == id);

            var coffeeIngredient = coffeeType.CoffeeIngredient;

            _context.CoffeeIngredients.Remove(coffeeIngredient);

            _context.CoffeeTypes.Remove(coffeeType);
            await _context.SaveChangesAsync();
        }
    }
}
