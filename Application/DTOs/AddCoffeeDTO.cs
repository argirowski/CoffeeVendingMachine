namespace Application.DTOs
{
    public class AddCoffeeDTO
    {
        public required string Name { get; set; }
        public required CoffeeIngredientDTO CoffeeIngredient { get; set; }
    }
}
