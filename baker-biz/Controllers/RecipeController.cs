using System.Text.Json;
using System.Text.Json.Serialization;
using baker_biz.Models;
using baker_biz_interfaces.Controllers;
using baker_biz_interfaces.Models;

namespace baker_biz.Controllers
{
    public class RecipeController : IRecipeController
    {
        public  RecipeModel Recipe { get; set; } = new RecipeModel();

        public RecipeController(RecipeModel recipe)
        {
            Recipe = recipe;
        }

        // Calculate How many of each Version of the Recipe we can make
        public Dictionary<string, uint> ProcessRecipe()
        {
            Dictionary<string, uint> maxPies = new Dictionary<string, uint>();
            List<IngredientModel> requiredIngredients;
            List<IngredientModel> optionalIngredients;

            // Separate AmountRequired and Optional Ingredients
            (requiredIngredients, optionalIngredients) = sortIngredients(Recipe.Ingredients);

            // While all of the required activeIngredients still have enough left to make at least one more pie
            while (0 < calculatePies(requiredIngredients))
            {
                // For each optional ingredient remaining: if it is zero, or doesn't have enough left for a pie, remove it from the active optionalIngredients list
                optionalIngredients = cullIngredients(optionalIngredients);

                // Update the recipeVersion string
                string recipeVersion = buildRecipeVersionString(Recipe.Name, optionalIngredients);

                // Calculate the pies possible with the current version of the recipe
                uint currentRecipePiesPossible = Math.Min(calculatePies(requiredIngredients), calculatePies(optionalIngredients));

                // Add a new entry to the maxPies Dictionary for the current ingredient list
                maxPies.Add(recipeVersion, currentRecipePiesPossible);

                // For that minimum number, consume the required total of each ingredient
                consumeIngredients(maxPies[recipeVersion], requiredIngredients);
                consumeIngredients(maxPies[recipeVersion], optionalIngredients);
            }

            return maxPies;
        }

        private string buildRecipeVersionString(string recipeVersion, List<IngredientModel> optionalIngredients)
        {
            if (!optionalIngredients.Any())
            {
                return recipeVersion;
            }

            string newVersion = $"{recipeVersion} (With:";

            foreach(var inactive in optionalIngredients)
            {
                newVersion = $"{newVersion} {inactive.Name}";

                if(inactive != optionalIngredients.Last())
                {
                    newVersion = $"{newVersion},";
                }
            }

            newVersion = $"{newVersion})";

            return newVersion;
        }

        private List<IngredientModel> cullIngredients(IEnumerable<IngredientModel> activeIngredients)
        {
            List<IngredientModel> remainingIngredients = new List<IngredientModel>();

            foreach (var ingredient in activeIngredients)
            {
                if(ingredient.Supply >= ingredient.AmountRequired)
                {
                    remainingIngredients.Add(ingredient);
                }
            }

            return remainingIngredients;
        }

        private void consumeIngredients(uint maxPies, IEnumerable<IngredientModel> ingredients)
        {
            foreach(var ingredient in ingredients)
            {
                ingredient.Supply -= ingredient.AmountRequired * maxPies;
            }
        }

        private uint calculatePies(IEnumerable<IngredientModel> activeIngredients)
        {
            // For each ingredient, how many pies can we make?
            // Find the minimum number of pies possible
            uint piesPossible = ushort.MaxValue;

            foreach(var ingredient in activeIngredients)
            {
                if (ingredient.AmountRequired > 0)
                {
                    uint piesFromIngredient = ingredient.Supply / ingredient.AmountRequired;
                    piesPossible = Math.Min(piesPossible, piesFromIngredient);
                }
            }

            return piesPossible;
        }

        private (List<IngredientModel> requiredIngredients, List<IngredientModel> optionalIngredients) sortIngredients(List<IngredientModel> ingredients)
        {
            List<IngredientModel> requiredIngredients = new List<IngredientModel>();
            List<IngredientModel> optionalIngredients = new List<IngredientModel>();

            foreach (var ingredient in ingredients)
            {
                if(ingredient.Optional)
                {
                    optionalIngredients.Add(ingredient);
                }
                else
                {
                    requiredIngredients.Add(ingredient);
                }
            }

            return (requiredIngredients, optionalIngredients);
        }

        public Dictionary<string, IIngredientModel> GetLeftovers()
        {
            Dictionary<string, IIngredientModel> leftovers = new Dictionary<string, IIngredientModel>();

            foreach(var ingredient in Recipe.Ingredients)
            {
                leftovers.Add(ingredient.Name, ingredient);
            }

            return leftovers;
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