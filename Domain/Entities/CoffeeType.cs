namespace Domain.Entities
{
    public class CoffeeType
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }

        public Guid CoffeeIngredientId { get; set; }
        public CoffeeIngredient? CoffeeIngredient { get; set; }
    }
}
