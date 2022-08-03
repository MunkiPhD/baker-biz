using System;

namespace BakerBiz
{
    public class ApplePieRecipe : IPieRecipe
    {
        public IList<Ingredient> Ingredients { get; private set; }

        public string Name { get; private set; }

        public ApplePieRecipe()
        {
            Name = "Apple Pie";
            Ingredients = new List<Ingredient>(){
                new Ingredient(IngredientType.Apples, 3, Units.number),
                new Ingredient(IngredientType.Sugar, 2, Units.lbs),
                new Ingredient(IngredientType.Flour, 1, Units.lbs),
                new Ingredient(IngredientType.Cinnamon, 1, Units.tsp, false),
            };
        }


    }
}
