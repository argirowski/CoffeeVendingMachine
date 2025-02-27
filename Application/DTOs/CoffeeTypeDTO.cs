namespace Application.DTOs
{
    public class CoffeeTypeDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public CoffeeIngredientDTO CoffeeIngredient { get; set; }
    }
}
