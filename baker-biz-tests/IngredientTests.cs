using baker_biz.Models;
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
            IngredientModel salt = new IngredientModel
            {
                Supply = 50,
                UnitConversionConstant = 10,
                PantryUnits = "Cartons"
            };

            Assert.That(salt.PantrySupply, Is.EqualTo(5));
        }

        [Test]
        public void PantryUnitsAreConvertedToRecipeUnits()
        {
            IngredientModel salt = new IngredientModel
            {
                UnitConversionConstant = 10,
                PantrySupply = 5,
                PantryUnits = "Cartons"
            };

            Assert.That(salt.Supply, Is.EqualTo(50));
        }
    }
}
