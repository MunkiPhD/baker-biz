using System.Text.Json;

namespace baker_biz.Models
{
    public class IngredientModel
    {
        public string Name { get; set; } = "";          // Name for this IngredientModel
        public string RecipeUnits { get; set; } = "";   // Name for units used by a recipe
        public uint UnitConversion { get; set; } = 1;   // recipe units per pantry unit
        public string PantryUnits { get; set; } = "";   // Name for units input to the pantry and reported at the end
    }
}