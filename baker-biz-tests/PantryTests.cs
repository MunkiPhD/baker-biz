using baker_biz_interfaces;
using bakerbiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baker_biz_tests
{
    internal class PantryTests
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
        public void UseIngredientConsumesRecipeUnitAmounts()
        {
            testPantry.UseIngredient("red", 56);
            Assert.That(((TestHarnessPantry)testPantry).GetSupplies()["red"].RecipeUnits, Is.EqualTo(200));

            testPantry.UseIngredient("blue", 200);
            Assert.That(((TestHarnessPantry)testPantry).GetSupplies()["blue"].RecipeUnits, Is.EqualTo(56));

            testPantry.UseIngredient("green", 128);
            Assert.That(((TestHarnessPantry)testPantry).GetSupplies()["green"].RecipeUnits, Is.EqualTo(128));
        }

        [Test]
        public void GetAmountRemainingReturnsRecipeUnits()
        {
            var supplies = ((TestHarnessPantry)testPantry).GetSupplies();

            supplies["red"].RecipeUnits = 5;
            Assert.That(testPantry.GetAmountRemaining("red"), Is.EqualTo(5));

            supplies["green"].RecipeUnits = 6;
            Assert.That(testPantry.GetAmountRemaining("green"), Is.EqualTo(6));

            supplies["blue"].RecipeUnits = 7;
            Assert.That(testPantry.GetAmountRemaining("blue"), Is.EqualTo(7));
        }

        [Test]
        public void UsingTooMuchOfAnIngredientThrowsAnException()
        {
            Assert.That(() => testPantry.UseIngredient("red", 300), Throws.Exception.TypeOf<OverflowException>());
        }
    }
}
