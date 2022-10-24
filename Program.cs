using System;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace bakerbiz
{
    class Program
    {
        static void Main(string[] args)
        {
            Pantry mainPantry = new Pantry("ingredients.json");

            IEnumerable<Recipe> recipeBook = LoadRecipes("recipes");

            foreach (var recipe in recipeBook)
            {
                recipe.Calc(mainPantry);
                recipe.Report();
            }

            mainPantry.ReportLeftOvers();

        }

        private static List<Recipe> LoadRecipes(string dir)
        {
            List<Recipe> recipeBook = new List<Recipe>();

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

        private static Recipe LoadRecipe(string rec)
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
