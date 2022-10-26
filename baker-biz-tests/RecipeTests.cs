using bakerbiz;
using baker_biz_interfaces;

namespace baker_biz_tests
{
    public class RecipeTests
    {
        IPantry testPantry;

        [SetUp]
        public void Setup()
        {
            testPantry = new TestHarnessPantry(new Dictionary<string, Ingredient>
            {
                { "red", new Ingredient {
                        PantryUnits = 256,
                        PantryUnitDescription = "red lights"
                    } 
                },
                { "green", new Ingredient {
                        UnitConversion = 4,
                        PantryUnits = 64,
                        PantryUnitDescription = "green lights"
                    }
                },
                { "blue", new Ingredient {
                        UnitConversion = 2,
                        PantryUnits = 128,
                        PantryUnitDescription = "blue lights"
                    }
                }
            });
        }

        [Test]
        public void ARecipeWithNoIngredientsWillUseNoSupplies()
        {
            Recipe noIngredients = new Recipe {
                Name = "No Ingredients"
            };

            noIngredients.Calc(testPantry);

            // Assert Recipe Units are correct
            Assert.That(testPantry.GetAmountRemaining("red"), Is.EqualTo(256));
            Assert.That(testPantry.GetAmountRemaining("green"), Is.EqualTo(256));
            Assert.That(testPantry.GetAmountRemaining("blue"), Is.EqualTo(256));

            // Assert that the Pantry Units are correct
            var supplies = ((TestHarnessPantry)testPantry).GetSupplies();

            Assert.That(supplies["red"].PantryUnits, Is.EqualTo(256));
            Assert.That(supplies["green"].PantryUnits, Is.EqualTo(64));
            Assert.That(supplies["blue"].PantryUnits, Is.EqualTo(128));
        }

        [Test]
        public void RecipeUsesCorrectVolumesOfIngredients()
        {
            Recipe skyBlue = new Recipe {
                Name = "Sky Blue",
                Ingredients = {
                    { "red", 66 },
                    { "green", 135 },
                    { "blue", 245 }
                }
            };

            skyBlue.Calc(testPantry);
            // We can make one serving from the test ingredients

            // Assert Recipe Units are correct
            Assert.That(testPantry.GetAmountRemaining("red"), Is.EqualTo(190));
            Assert.That(testPantry.GetAmountRemaining("green"), Is.EqualTo(121));
            Assert.That(testPantry.GetAmountRemaining("blue"), Is.EqualTo(11));

            // Assert that the Pantry Units are correct
            var supplies = ((TestHarnessPantry) testPantry).GetSupplies();

            Assert.That(supplies["red"].PantryUnits, Is.EqualTo(190));
            Assert.That(supplies["green"].PantryUnits, Is.EqualTo(30.25));
            Assert.That(supplies["blue"].PantryUnits, Is.EqualTo(5.5));
        }
    }
}