using BakerBiz;

namespace Baker_Biz.Tests;

public class PieCalculatorTests{

    [Fact]
    public void CalculateNumPies_TestWithCinnamon(){
        IPieRecipe recipe = new ApplePieRecipe();

        recipe.Ingredients.Single(x => x.Type == IngredientType.Apples).Amount = 6;
        recipe.Ingredients.Single(x => x.Type == IngredientType.Sugar).Amount = 4; 
        recipe.Ingredients.Single(x => x.Type == IngredientType.Flour).Amount = 2;
        recipe.Ingredients.Single(x => x.Type == IngredientType.Cinnamon).Amount = 2;       
        int numPies = PieCalculator.CalculateNumPies(recipe);
        Assert.True(numPies == 2, "Failed to calculate number of pies");
    }

    [Fact]
    public void CalculateNumPies_TestWithoutCinnamon(){
        IPieRecipe recipe = new ApplePieRecipe();

        recipe.Ingredients.Single(x => x.Type == IngredientType.Apples).Amount = 6;
        recipe.Ingredients.Single(x => x.Type == IngredientType.Sugar).Amount = 4; 
        recipe.Ingredients.Single(x => x.Type == IngredientType.Flour).Amount = 2;
        recipe.Ingredients.Single(x => x.Type == IngredientType.Cinnamon).Amount = 0;       
        int numPies = PieCalculator.CalculateNumPies(recipe);
        Assert.True(numPies == 2, "Failed to calculate number of pies");
    }
}