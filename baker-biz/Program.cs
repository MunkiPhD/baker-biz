using baker_biz;
using baker_biz_interfaces;
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
            IEnumerable<IRecipe> recipeBook = LoadRecipes("recipes");

            IUserInterface ui = new CLI();

            // Select which recipe to process
            IRecipe? recipe = ui.SelectRecipe(recipeBook);

            if (recipe != null)
            {
                // Process the recipe

                // Report results of recipe calculation
            }
        }

        private static List<IRecipe> LoadRecipes(string dir)
        {
            List<IRecipe> recipeBook = new List<IRecipe>();

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

        private static IRecipe LoadRecipe(string rec)
        {
            string jsonString = File.ReadAllText(rec);
            var options = new JsonSerializerOptions
            {
                Converters =
                {
                    new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
                }
            };
            return JsonSerializer.Deserialize<Recipe>(jsonString, options) ?? new Recipe();
        }
    }
}
