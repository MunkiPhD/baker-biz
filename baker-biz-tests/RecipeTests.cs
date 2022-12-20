using bakerbiz;
using baker_biz.Models;
using baker_biz.Controllers;
using baker_biz_interfaces.Controllers;
using baker_biz_interfaces.Models;

namespace baker_biz_tests
{
    public class RecipeTests
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void RecipeUsesCorrectVolumesOfIngredients()
        {
            RecipeController skyBlue = new RecipeController(new RecipeModel()
            {
                Name = "Sky Blue",
                Ingredients = new List<IngredientModel>
                {
                    {
                        new IngredientModel()
                        {
                            Name = "red",
                            AmountRequired = 66,
                            PantrySupply = 256,
                            PantryUnits = "red lights"
                        }
                    },
                    {
                        new IngredientModel
                        {
                            Name = "green",
                            UnitConversionConstant = 4,
                            AmountRequired = 135,
                            PantrySupply = 64,
                            PantryUnits = "green lights"
                        }
                    },
                    {
                        new IngredientModel
                        {
                            Name = "blue",
                            UnitConversionConstant = 2,
                            AmountRequired = 245,
                            PantrySupply = 128,
                            PantryUnits = "blue lights"
                        }
                    }
                }
            });

            skyBlue.ProcessRecipe();
            // We can make one serving from the test ingredients

            Dictionary<string, IIngredientModel> remainingIngredients = skyBlue.GetLeftovers();

            // Assert RecipeController Units are correct
            Assert.That(remainingIngredients["red"].Supply,     Is.EqualTo(190));
            Assert.That(remainingIngredients["green"].Supply,   Is.EqualTo(121));
            Assert.That(remainingIngredients["blue"].Supply,    Is.EqualTo(11));

            // Assert that the Pantry Units are correct
            Assert.That(remainingIngredients["red"].PantrySupply,   Is.EqualTo(190));
            Assert.That(remainingIngredients["green"].PantrySupply, Is.EqualTo(30.25));
            Assert.That(remainingIngredients["blue"].PantrySupply, Is.EqualTo(5.5));
        }
    }
}