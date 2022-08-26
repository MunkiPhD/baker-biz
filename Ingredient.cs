namespace bakerbiz
{
    public enum Ingredient_Type
    {
        apple,
        sugar,
        flour,
        cinnamon
    }

    public class Ingredient
    {
        private readonly string gatherMessage;
        public string Name {get; set;}
        public int AmountRemaining { get; set; }
        public string Units { get; set; }
        public string GatherMessage { get { return gatherMessage; } }

        public Ingredient(string name, string unit, string gatherMess)
        {
            Name = name;
            Units = unit;
            gatherMessage = gatherMess;
        }
    }
}