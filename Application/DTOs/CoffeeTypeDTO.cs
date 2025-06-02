namespace Application.DTOs
{
    public class CoffeeTypeDTO
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required CoffeeIngredientDTO CoffeeIngredient { get; set; }
    }
}
