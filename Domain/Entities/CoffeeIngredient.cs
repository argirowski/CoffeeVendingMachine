namespace Domain.Entities
{
    public class CoffeeIngredient
    {
        public Guid Id { get; set; }
        public int DosesOfMilk { get; set; }
        public int PacksOfSugar { get; set; }
        public bool Cinnamon { get; set; }
        public bool Stevia { get; set; }
        public bool CoconutMilk { get; set; }
        public CoffeeType? CoffeeType { get; set; }
    }
}
