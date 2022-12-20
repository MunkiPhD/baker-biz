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

            Dictionary<string, uint> results = skyBlue.ProcessRecipe();

            // We can make one serving from the test ingredients
            Assert.That(results.Count(), Is.EqualTo(1));
            Assert.That(results["Sky Blue"], Is.EqualTo(1));

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

        [Test]
        public void RecipeHandlesOptionalIngredients()
        {
            RecipeController milk = new RecipeController(new RecipeModel()
            {
                Name = "Milk",
                Ingredients = new List<IngredientModel>
                {
                    {
                        new IngredientModel()
                        {
                            Name = "Milk",
                            AmountRequired = 1,
                            PantrySupply = 5,
                            PantryUnits = "cup"
                        }
                    },
                    {
                        new IngredientModel
                        {
                            Name = "Chocolate",
                            AmountRequired = 3,
                            PantrySupply = 4,
                            PantryUnits = "oz",
                            Optional = true
                        }
                    }
                }
            });

            Dictionary<string, uint> results = milk.ProcessRecipe();

            // We can make 1 milks with chocolate, and 4 plain milks from the test ingredients
            Assert.That(results.Count(), Is.EqualTo(2));
            Assert.That(results["Milk"], Is.EqualTo(4));
            Assert.That(results["Milk (With: Chocolate)"], Is.EqualTo(1));


            Dictionary<string, IIngredientModel> remainingIngredients = milk.GetLeftovers();
            // We will have no milk leftover and 2 chocolate leftover
            Assert.That(remainingIngredients["Milk"].Supply, Is.EqualTo(0));
            Assert.That(remainingIngredients["Chocolate"].Supply, Is.EqualTo(1));

            // The same goes for the pantry supply of each
            Assert.That(remainingIngredients["Milk"].PantrySupply, Is.EqualTo(0));
            Assert.That(remainingIngredients["Chocolate"].PantrySupply, Is.EqualTo(1));
        }
    }
}