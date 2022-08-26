using System;

namespace bakerbiz
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<Ingredient_Type, Ingredient> allIngredients = new Dictionary<Ingredient_Type, Ingredient>() {
                { Ingredient_Type.apple,    new Ingredient("apple",     "",     "How many apples do you have?") },
                { Ingredient_Type.sugar,    new Ingredient("sugar",     "lbs",  "How many lbs of sugar do you have?") },
                { Ingredient_Type.flour,    new Ingredient("flour",     "lbs",  "How many lbs of flour do you have?") },
                { Ingredient_Type.cinnamon, new Ingredient("cinnamon",  "tsp",  "How many tsp of cinnamon do you have?") }
            };

            Pantry mainPantry = new Pantry(allIngredients);

            mainPantry.GatherIngredients();

            BakeCinnamonPies(mainPantry);

            BakeBasicPies(mainPantry);

            mainPantry.ReportLeftOvers();

        }

        private static void BakeCinnamonPies(Pantry pantry)
        {
            Dictionary<Ingredient_Type, int> cinnamonPieIngredients = new Dictionary<Ingredient_Type, int>() {
                { Ingredient_Type.apple,    3 },
                { Ingredient_Type.sugar,    2 },
                { Ingredient_Type.flour,    1 },
                { Ingredient_Type.cinnamon, 1 }
            };

            Recipe cinnamonPieRecipe = new Recipe("Cinamon Apple", cinnamonPieIngredients);
            cinnamonPieRecipe.Calc(pantry);
            cinnamonPieRecipe.Report();
        }

        private static void BakeBasicPies(Pantry pantry)
        {
            Dictionary<Ingredient_Type, int> basicPieIngredients = new Dictionary<Ingredient_Type, int>() {
                { Ingredient_Type.apple, 3 },
                { Ingredient_Type.sugar, 2 },
                { Ingredient_Type.flour, 1 }
            };
            Recipe basicPieRecipe = new Recipe("Apple", basicPieIngredients);
            basicPieRecipe.Calc(pantry);
            basicPieRecipe.Report();
        }
    }
}
