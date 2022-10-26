using System.Text.Json;
using System.Text.Json.Serialization;
using baker_biz_interfaces;

namespace bakerbiz
{
    public class Recipe : IRecipe
    {
        private uint maxPies = UInt16.MaxValue;

        public string? Name {get; set;}
        public Dictionary<string, uint> Ingredients {get; set;} = new Dictionary<string, uint>();

        public Recipe() {}

        public void Calc(IPantry pantry)
        {
            CalcPieCounts(pantry);

            CalcLeftOvers(pantry);
        }

        protected void CalcPieCounts(IPantry pantry)
        {
            foreach(var i in Ingredients)
            {
                double piesDouble = pantry.GetAmountRemaining(i.Key) / i.Value;
                uint piesFromIngredient = Convert.ToUInt16(piesDouble);
                maxPies = Math.Min(maxPies, piesFromIngredient);
            }
        }

        protected void CalcLeftOvers(IPantry pantry)
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