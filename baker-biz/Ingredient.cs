using System.Text.Json;

namespace bakerbiz
{
    public class Ingredient
    {
        public string RecipeUnits { get; set; } = "";   // Name for units used by a recipe
        public uint UnitConversion { get; set; } = 1;   // recipe units per pantry unit
        public string PantryUnits { get; set; } = "";   // Name for units input to the pantry and reported at the end
    }
}