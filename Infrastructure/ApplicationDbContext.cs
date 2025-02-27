using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
        {
        }

        public DbSet<CoffeeType> CoffeeTypes { get; set; }
        public DbSet<CoffeeIngredient> CoffeeIngredients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CoffeeType>()
                .HasOne(ct => ct.CoffeeIngredient)
                .WithOne(ci => ci.CoffeeType)
                .HasForeignKey<CoffeeType>(ct => ct.CoffeeIngredientId)
                .OnDelete(DeleteBehavior.Cascade);

            // Call the Seed method
            modelBuilder.Seed();

            // Call the base OnModelCreating method
            base.OnModelCreating(modelBuilder);
        }
    }
}

