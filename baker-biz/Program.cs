using baker_biz;
using baker_biz.Controllers;
using baker_biz.Models;
using baker_biz.Views;
using baker_biz_interfaces.Controllers;
using baker_biz_interfaces.Views;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace bakerbiz
{
    class Program
    {
        static void Main(string[] args)
        {
            // Load Recipes
            IEnumerable<IRecipeController> recipeBook = LoadRecipes("recipes");

            IUserInterface ui = new CLI();

            // Select which recipe to process
            IRecipeController? recipe = ui.SelectRecipe(recipeBook);

            if (recipe != null)
            {
                // Report results of recipe calculation
                ui.ReportCalculationResults(recipe);
            }
        }

        private static List<IRecipeController> LoadRecipes(string dir)
        {
            List<IRecipeController> recipeBook = new List<IRecipeController>();

            try
            {
                var recipeFiles = Directory.EnumerateFiles(dir, "*.rec", SearchOption.AllDirectories);

                foreach (string recipe in recipeFiles)
                {
                    try
                    {
                        recipeBook.Add(LoadRecipe(recipe));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"In file {recipe} error: {e.Message}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return recipeBook;
        }

        private static IRecipeController LoadRecipe(string rec)
        {
            string jsonString = File.ReadAllText(rec);
            var options = new JsonSerializerOptions
            {
                Converters =
                {
                    new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
                }
            };
            return new RecipeController(JsonSerializer.Deserialize<RecipeModel>(jsonString, options) ?? new RecipeModel());
        }
    }
}
