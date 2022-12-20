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

        #region Validation Tests
        [Test]
        public void RecipesMustHaveAName()
        {
            RecipeController testRecipe = new RecipeController(new RecipeModel()
            {
                Name = "",
                Ingredients = new List<IngredientModel>
                {
                    {
                        new IngredientModel()
                        {
                            Name = "a",
                            AmountRequired = 1,
                            PantrySupply = 5,
                            PantryUnits = "b"
                        }
                    }
                }
            });

            List<string> errors = testRecipe.Validate();

            Assert.That(errors.Contains("Recipes cannot have a null or empty name!"), Is.True);
        }

        [Test]
        public void RecipesMustHaveAtLeastOneIngredient()
        {
            RecipeController testRecipe = new RecipeController(new RecipeModel()
            {
                Name = "No Ingredients"
            });

            List<string> errors = testRecipe.Validate();

            Assert.That(errors.Contains("Recipes must have at least one ingredient!"), Is.True);
        }

        [Test]
        public void IngredientsMustHaveAName()
        {
            RecipeController testRecipe = new RecipeController(new RecipeModel()
            {
                Name = "Ingredient without a name",
                Ingredients = new List<IngredientModel>
                {
                    {
                        new IngredientModel()
                        {
                            Name = "",
                            AmountRequired = 1,
                            PantrySupply = 5,
                            PantryUnits = "b"
                        }
                    }
                }
            });

            List<string> errors = testRecipe.Validate();

            Assert.That(errors.Contains("Ingredients cannot have a null or empty name!"), Is.True);
        }

        [Test]
        public void IngredientNamesMustBeUnique()
        {
            RecipeController testRecipe = new RecipeController(new RecipeModel()
            {
                Name = "Ingredient without a duplicate name",
                Ingredients = new List<IngredientModel>
                {
                    {
                        new IngredientModel()
                        {
                            Name = "a",
                            AmountRequired = 1,
                            PantrySupply = 5,
                            PantryUnits = "b"
                        }
                    },
                    {
                        new IngredientModel()
                        {
                            Name = "a",
                            AmountRequired = 1,
                            PantrySupply = 5,
                            PantryUnits = "b"
                        }
                    }
                }
            });

            List<string> errors = testRecipe.Validate();

            Assert.That(errors.Contains("a is already present in the ingredients list!"), Is.True);
        }

        [Test]
        public void IngredientsConversionConstantCannotBeZero()
        {
            RecipeController testRecipe = new RecipeController(new RecipeModel()
            {
                Name = "Conversion Constant is 0",
                Ingredients = new List<IngredientModel>
                {
                    {
                        new IngredientModel()
                        {
                            Name = "a",
                            AmountRequired = 1,
                            PantrySupply = 5,
                            PantryUnits = "b",
                            UnitConversionConstant = 0
                        }
                    }
                }
            });

            List<string> errors = testRecipe.Validate();

            Assert.That(errors.Contains("a has a conversion constant of 0, this is invalid!"), Is.True);
        }

        [Test]
        public void IngredientAmountRequiredCannotBeZero()
        {
            RecipeController testRecipe = new RecipeController(new RecipeModel()
            {
                Name = "Ingredient without a name",
                Ingredients = new List<IngredientModel>
                {
                    {
                        new IngredientModel()
                        {
                            Name = "a",
                            AmountRequired = 0,
                            PantrySupply = 5,
                            PantryUnits = "b"
                        }
                    }
                }
            });

            List<string> errors = testRecipe.Validate();

            Assert.That(errors.Contains("a is a required ingredient that requires 0 units, this is invalid!"), Is.True);
        }
        #endregion
    }
}