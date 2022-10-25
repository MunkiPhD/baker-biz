using bakerbiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baker_biz_tests
{
    internal class IngredientTests
    {
        [Test]
        public void PantryUnitsAreConvertedFromRecipeUnits()
        {
            Ingredient salt = new Ingredient
            {
                RecipeUnits = 50,
                UnitConversion = 10,
                PantryUnitDescription = "Cartons"
            };

            Assert.That(salt.PantryUnits, Is.EqualTo(5));
        }

        [Test]
        public void PantryUnitsAreConvertedToRecipeUnits()
        {
            Ingredient salt = new Ingredient
            {
                UnitConversion = 10,
                PantryUnits = 5,
                PantryUnitDescription = "Cartons"
            };

            Assert.That(salt.RecipeUnits, Is.EqualTo(50));
        }
    }
}
