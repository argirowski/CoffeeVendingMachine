namespace Application.DTOs
{
    public class UpdateCoffeeDTO
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required CoffeeIngredientDTO CoffeeIngredient { get; set; }
    }
}
