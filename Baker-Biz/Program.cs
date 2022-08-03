using System;

namespace BakerBiz
{
    class Program
    {
        static void Main(string[] args)
        {
            // want to maximize the number of apple pies we can make.
            // it takes 3 apples, 2 lbs of sugar and 1 pound of flour to make 1 apple pie
            // this is intended to run on .NET Core

            ApplePieRecipe applePie = new ApplePieRecipe();
            do
            {
                foreach (Ingredient ingredient in applePie.Ingredients)
                {
                    Console.WriteLine($"Enter the {ingredient.Units} of {ingredient.Type}");
                    var amountEntered = Console.ReadLine();
                    int amount;
                    if (!ParseInputStrings(amountEntered, ingredient.Type, out amount))
                    {
                        continue;
                    }
                    else
                    {
                        ingredient.Amount = amount;
                    }
                }

                int pieCount = PieCalculator.CalculateNumPies(applePie);

                Console.WriteLine("You can make:");
                Console.WriteLine(pieCount + " " + applePie.Name);

                PrintLeftovers(applePie, pieCount);
                Console.WriteLine("\n\nEnter to calculate, 'q' to quit!");

            } while (!string.Equals(Console.ReadLine(), "Q", StringComparison.OrdinalIgnoreCase));

        }

        private static void PrintLeftovers(IPieRecipe recipe, int pieCount)
        {
            if (recipe != null && recipe.Ingredients.Any())
            {
                foreach (Ingredient ingredient in recipe.Ingredients)
                {
                    Console.WriteLine($"{ingredient.CalculateLeftovers(pieCount)} {ingredient.Units}(s) {ingredient.Type} left over.");
                }
            }
        }

        private static bool ParseInputStrings(string input, IngredientType ingredientType, out int result)
        {
            if (string.IsNullOrEmpty(input))
            {
                result = 0;
                return false;
            }

            bool success = int.TryParse(input, out result);

            if (!success)
            {
                Console.WriteLine($"I do not understand \"{input}\" {ingredientType}. What does that even mean?  Come on MAN!!!");
                Console.WriteLine("Let's try again please, from the beginning.  Hit <enter>");
            }

            return success;
        }
    }




}
