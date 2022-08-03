using BakerBiz;

namespace Baker_Biz.Tests;

public class IngredientTests
{
    [Fact]
    public void CalculateWholePies_TestRequired()
    {
        Ingredient ingredient = new Ingredient(IngredientType.Apples, 2, Units.number);
        ingredient.Amount = 7;

        Assert.True(ingredient.CalculateWholePies() == 3, "Failed to calculate 3 pies.");

    }

    [Fact]
    public void CalculateWholePies_TestNotRequired()
    {
        Ingredient ingredient = new Ingredient(IngredientType.Apples, 2, Units.number, false);
        ingredient.Amount = 0;

        Assert.True(ingredient.CalculateWholePies() == int.MaxValue, "Failed to handle optional ingredient.");

    }

    [Fact]
    public void CalculateLeftovers_Test(){
        Ingredient ingredient = new Ingredient(IngredientType.Flour, 3, Units.lbs, true);
        ingredient.Amount = 11;

        Assert.True(ingredient.CalculateLeftovers(3) == 2, "Failed to calculate 2 lbs leftover");
    }
}