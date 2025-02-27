using Application.DTOs;
using Application.Mapping;
using AutoMapper;
using Domain.Entities;
using Xunit;

namespace MappingProfileTests
{
    public class MappingProfileTests
    {
        private readonly IMapper _mapper;

        public MappingProfileTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = config.CreateMapper();
        }

        [Fact]
        public void Should_Have_Valid_Configuration()
        {
            // Arrange & Act
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            // Assert
            config.AssertConfigurationIsValid();
        }

        [Fact]
        public void Should_Map_CoffeeType_To_CoffeeTypeDTO()
        {
            // Arrange
            var coffeeType = new CoffeeType
            {
                Id = Guid.NewGuid(),
                Name = "Espresso",
                CoffeeIngredient = new CoffeeIngredient
                {
                    Id = Guid.NewGuid(),
                    DosesOfMilk = 1,
                    PacksOfSugar = 2,
                    Cinnamon = true,
                    Stevia = false,
                    CoconutMilk = true
                }
            };

            // Act
            var coffeeTypeDTO = _mapper.Map<CoffeeTypeDTO>(coffeeType);

            // Assert
            Assert.Equal(coffeeType.Id, coffeeTypeDTO.Id);
            Assert.Equal(coffeeType.Name, coffeeTypeDTO.Name);
            Assert.Equal(coffeeType.CoffeeIngredient.DosesOfMilk, coffeeTypeDTO.CoffeeIngredient.DosesOfMilk);
            Assert.Equal(coffeeType.CoffeeIngredient.PacksOfSugar, coffeeTypeDTO.CoffeeIngredient.PacksOfSugar);
            Assert.Equal(coffeeType.CoffeeIngredient.Cinnamon, coffeeTypeDTO.CoffeeIngredient.Cinnamon);
            Assert.Equal(coffeeType.CoffeeIngredient.Stevia, coffeeTypeDTO.CoffeeIngredient.Stevia);
            Assert.Equal(coffeeType.CoffeeIngredient.CoconutMilk, coffeeTypeDTO.CoffeeIngredient.CoconutMilk);
        }

        [Fact]
        public void Should_Map_CoffeeTypeDTO_To_CoffeeType()
        {
            // Arrange
            var coffeeTypeDTO = new CoffeeTypeDTO
            {
                Id = Guid.NewGuid(),
                Name = "Espresso",
                CoffeeIngredient = new CoffeeIngredientDTO
                {
                    DosesOfMilk = 1,
                    PacksOfSugar = 2,
                    Cinnamon = true,
                    Stevia = false,
                    CoconutMilk = true
                }
            };

            // Act
            var coffeeType = _mapper.Map<CoffeeType>(coffeeTypeDTO);

            // Assert
            Assert.Equal(coffeeTypeDTO.Id, coffeeType.Id);
            Assert.Equal(coffeeTypeDTO.Name, coffeeType.Name);
            Assert.Equal(coffeeTypeDTO.CoffeeIngredient.DosesOfMilk, coffeeType.CoffeeIngredient.DosesOfMilk);
            Assert.Equal(coffeeTypeDTO.CoffeeIngredient.PacksOfSugar, coffeeType.CoffeeIngredient.PacksOfSugar);
            Assert.Equal(coffeeTypeDTO.CoffeeIngredient.Cinnamon, coffeeType.CoffeeIngredient.Cinnamon);
            Assert.Equal(coffeeTypeDTO.CoffeeIngredient.Stevia, coffeeType.CoffeeIngredient.Stevia);
            Assert.Equal(coffeeTypeDTO.CoffeeIngredient.CoconutMilk, coffeeType.CoffeeIngredient.CoconutMilk);
        }

        [Fact]
        public void Should_Map_CoffeeIngredient_To_CoffeeIngredientDTO()
        {
            // Arrange
            var coffeeIngredient = new CoffeeIngredient
            {
                Id = Guid.NewGuid(),
                DosesOfMilk = 1,
                PacksOfSugar = 2,
                Cinnamon = true,
                Stevia = false,
                CoconutMilk = true
            };

            // Act
            var coffeeIngredientDTO = _mapper.Map<CoffeeIngredientDTO>(coffeeIngredient);

            // Assert
            Assert.Equal(coffeeIngredient.DosesOfMilk, coffeeIngredientDTO.DosesOfMilk);
            Assert.Equal(coffeeIngredient.PacksOfSugar, coffeeIngredientDTO.PacksOfSugar);
            Assert.Equal(coffeeIngredient.Cinnamon, coffeeIngredientDTO.Cinnamon);
            Assert.Equal(coffeeIngredient.Stevia, coffeeIngredientDTO.Stevia);
            Assert.Equal(coffeeIngredient.CoconutMilk, coffeeIngredientDTO.CoconutMilk);
        }

        [Fact]
        public void Should_Map_CoffeeIngredientDTO_To_CoffeeIngredient()
        {
            // Arrange
            var coffeeIngredientDTO = new CoffeeIngredientDTO
            {
                DosesOfMilk = 1,
                PacksOfSugar = 2,
                Cinnamon = true,
                Stevia = false,
                CoconutMilk = true
            };

            // Act
            var coffeeIngredient = _mapper.Map<CoffeeIngredient>(coffeeIngredientDTO);

            // Assert
            Assert.Equal(coffeeIngredientDTO.DosesOfMilk, coffeeIngredient.DosesOfMilk);
            Assert.Equal(coffeeIngredientDTO.PacksOfSugar, coffeeIngredient.PacksOfSugar);
            Assert.Equal(coffeeIngredientDTO.Cinnamon, coffeeIngredient.Cinnamon);
            Assert.Equal(coffeeIngredientDTO.Stevia, coffeeIngredient.Stevia);
            Assert.Equal(coffeeIngredientDTO.CoconutMilk, coffeeIngredient.CoconutMilk);
        }
    }
}