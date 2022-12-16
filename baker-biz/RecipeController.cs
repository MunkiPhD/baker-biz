using System.Text.Json;
using System.Text.Json.Serialization;
using baker_biz.Models;
using baker_biz_interfaces;

namespace bakerbiz
{
    public class RecipeController : IRecipeController
    {
        private uint maxPies = UInt16.MaxValue;
        internal RecipeModel Recipe { get; set; } = new RecipeModel();

        internal RecipeController(RecipeModel recipe)
        {
            Recipe = recipe;
        }

        public void ProcessRecipe()
        {
            // Calculate the leftovers for this recipe
        }


























        //public void Calc(IPantry pantry)
        //{
        //    CalcPieCounts(pantry);

        //    CalcLeftOvers(pantry);
        //}

        //protected void CalcPieCounts(IPantry pantry)
        //{
        //    foreach(var i in Ingredients)
        //    {
        //        double piesDouble = pantry.GetAmountRemaining(i.Key) / i.Value;
        //        uint piesFromIngredient = Convert.ToUInt16(piesDouble);
        //        maxPies = Math.Min(maxPies, piesFromIngredient);
        //    }
        //}

        //protected void CalcLeftOvers(IPantry pantry)
        //{
        //    foreach(var i in Ingredients)
        //    {
        //        pantry.UseIngredient(i.Key, (maxPies * i.Value));
        //    }
        //}

        //public void Report()
        //{
        //    Console.WriteLine($"You can make {maxPies} {Name}(s).");
        //}
    }
}