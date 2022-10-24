using System.Text.Json;
using System.Text.Json.Serialization;

namespace bakerbiz
{
    public class Recipe
    {
        private int maxPies = Int16.MaxValue;

        public string? Name {get; set;}
        public Dictionary<string, int> Ingredients {get; set;} = new Dictionary<string, int>();

        public Recipe() {}

        public void Calc(Pantry pantry)
        {
            CalcPieCounts(pantry);

            CalcLeftOvers(pantry);
        }

        private void CalcPieCounts(Pantry pantry)
        {
            foreach(var i in Ingredients)
            {
                double piesDouble = pantry.GetAmountRemaining(i.Key) / i.Value;
                int piesFromIngredient = Convert.ToInt16(piesDouble);
                maxPies = Math.Min(maxPies, piesFromIngredient);
            }
        }

        private void CalcLeftOvers(Pantry pantry)
        {
            foreach(var i in Ingredients)
            {
                pantry.UseIngredient(i.Key, (maxPies * i.Value));
            }
        }

        public void Report()
        {
            Console.WriteLine($"You can make {maxPies} {Name}(s).");
        }
    }
}