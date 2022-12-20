using baker_biz.Controllers;
using baker_biz.Models;
using baker_biz_interfaces.Controllers;
using baker_biz_interfaces.Models;
using baker_biz_interfaces.Views;
using bakerbiz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baker_biz.Views
{
    internal class CLI : IUserInterface
    {
        public IRecipeController? SelectRecipe(IEnumerable<IRecipeController> recipes)
        {
            RecipeController? recipe = null;

            if (recipes.Count() > 0)
            {
                // Print List of recipes with a number to reference them by
                Console.WriteLine("The following Items are available to calculate. Select which one to process using the arrow keys (Enter 'q' to quit):");

                int idx = SelectRecipeOption(recipes);

                recipe = recipes.ElementAt(idx) as RecipeController;

                if (recipe != null)
                {
                    Console.Clear();
                    Console.WriteLine($"{recipe.Recipe.Name} Selected, Processing Now...");

                    // Collect the availability of the Ingredients required by the selected recipe
                    CollectIngredients(recipe);
                }
            }
            else
            {
                Console.WriteLine("There are no Recipes to process. Please create some, and then run this program again!");
            }

            // Return the selected recipe from the list
            return recipe;
        }

        private int SelectRecipeOption(IEnumerable<IRecipeController> recipes)
        {
            var cursorPos = Console.GetCursorPosition();

            const int startX = 15;
            int startY = cursorPos.Top;

            int currentSelection = 0;

            ConsoleKey key;

            Console.CursorVisible = false;

            do
            {
                int row = 0;
                foreach (RecipeController rec in recipes)
                {
                    Console.SetCursorPosition(startX, startY + row);

                    if(row == currentSelection)
                    {
                        var oldForeGround = Console.ForegroundColor;
                        Console.ForegroundColor = Console.BackgroundColor;
                        Console.BackgroundColor = oldForeGround;
                    }

                    Console.Write($"{rec.Recipe.Name}");
                    Console.ResetColor();
                    row++;
                }

                key = Console.ReadKey(true).Key;

                switch(key)
                {
                    case ConsoleKey.UpArrow:    // Select previous option (looping around)
                        {
                            if(currentSelection > 0)
                            {
                                currentSelection--;
                            }
                            else
                            {
                                currentSelection = recipes.Count() - 1;
                            }
                            break;
                        }
                    case ConsoleKey.DownArrow:  // Select Next Option (looping arround)
                        {
                            if(currentSelection < (recipes.Count() - 1))
                            {
                                currentSelection++;
                            }
                            else
                            {
                                currentSelection = 0;
                            }
                            break;
                        }
                    case ConsoleKey.Q:
                    case ConsoleKey.Escape:
                        {
                            Console.Clear();
                            Console.WriteLine("Exiting BakerBiz Software...");
                            System.Environment.Exit(1);
                            break;
                        }
                    default:
                        break;
                }

            } while (key != ConsoleKey.Enter);

            return currentSelection;
        }

        private void CollectIngredients(RecipeController recipe)
        {
            Console.WriteLine("Enter the volume of supplies for each of the following ingredients:");

            foreach(IngredientModel ingredient in recipe.Recipe.Ingredients)
            {
                Console.CursorVisible = true;
                Console.Write($"{ingredient.Name} ({ingredient.PantryUnits}): ");
                string? count = Console.ReadLine();

                uint ingredientAmount;
                if(uint.TryParse(count, out ingredientAmount))
                {
                    ingredient.PantrySupply = ingredientAmount;
                }
            }
        }

        public void ReportCalculationResults(IRecipeController recipe)
        {
            // Validate the recipe
            List<string> errors = recipe.Validate();

            if(errors.Any())
            {

                foreach(string error in errors)
                {
                    Console.WriteLine(error);
                }
            }

            // Process the recipe
            Dictionary<string, uint> products = recipe.ProcessRecipe();

            // Report Recipes possible with optional ingredients
            reportProducts(products);

            // Report leftover ingredients
            reportLeftovers(recipe.GetLeftovers());
        }

        private void reportLeftovers(Dictionary<string, IIngredientModel> leftovers)
        {
            Console.WriteLine("You will have the following Ingredients Left Over:");

            foreach(var ingredient in leftovers)
            {
                Console.WriteLine($"{ingredient.Key}: {ingredient.Value.PantrySupply} {ingredient.Value.PantryUnits}");
            }
        }

        private void reportProducts(Dictionary<string, uint> products)
        {
            Console.Clear();

            Console.WriteLine("You can create the following:");

            foreach(var product in products)
            {
                Console.WriteLine($"{product.Key}: {product.Value}");
            }
        }

        public void ReportErrors(IEnumerable<string> errors)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("Erroneous Configuration detected, please fix it!");

            Console.BackgroundColor = ConsoleColor.Black;
            foreach(string error in errors)
            {
                Console.WriteLine(error);
            }
        }
    }
}
