using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public static class SeedData
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var espressoId = Guid.Parse("49ed75aa-0a08-40c8-92ed-cd88a68f564d");
            var americanoId = Guid.Parse("5cd91938-abc4-440f-b3c1-52371516bf8d");
            var caffeCremaId = Guid.Parse("8f516d6a-e1b3-4d66-b0d7-f9b40cdcdb04");

            var ingredient1Id = Guid.Parse("587cdc3e-40a8-43f1-b67e-251292d94f3e");
            var ingredient2Id = Guid.Parse("f0ca2d3e-554f-459e-9045-dce2d5ab616b");
            var ingredient3Id = Guid.Parse("ea3cf94a-7ab6-4347-a0a2-b8f32d2ba51b");

            modelBuilder.Entity<CoffeeType>().HasData(
                new CoffeeType
                {
                    Id = espressoId,
                    Name = "Espresso",
                    CoffeeIngredientId = ingredient1Id
                },
                new CoffeeType
                {
                    Id = americanoId,
                    Name = "Americano",
                    CoffeeIngredientId = ingredient2Id
                },
                new CoffeeType
                {
                    Id = caffeCremaId,
                    Name = "Caffè Crema",
                    CoffeeIngredientId = ingredient3Id
                }
            );

            modelBuilder.Entity<CoffeeIngredient>().HasData(
                new CoffeeIngredient
                {
                    Id = ingredient1Id,
                    DosesOfMilk = 1,
                    PacksOfSugar = 2,
                    Cinnamon = false,
                    Stevia = true,
                    CoconutMilk = false
                },
                new CoffeeIngredient
                {
                    Id = ingredient2Id,
                    DosesOfMilk = 0,
                    PacksOfSugar = 1,
                    Cinnamon = true,
                    Stevia = false,
                    CoconutMilk = true
                },
                new CoffeeIngredient
                {
                    Id = ingredient3Id,
                    DosesOfMilk = 2,
                    PacksOfSugar = 0,
                    Cinnamon = false,
                    Stevia = false,
                    CoconutMilk = true
                }
            );
        }
    }
}