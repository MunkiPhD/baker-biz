using System.Text.Json;

namespace bakerbiz
{
    public class Ingredient
    {
        public uint RecipeUnits { get; set; }            // units used by a recipe
        public uint UnitConversion { get; set; } = 1;    // recipe units per pantry unit
        public double PantryUnits                       // Units input to the pantry and reported at the end
        {
            get => Convert.ToDouble(RecipeUnits) / Convert.ToDouble(UnitConversion);
            set => RecipeUnits = Convert.ToUInt16(value * UnitConversion); 
        }
        public string? PantryUnitDescription { get; set; }    // Name for the pantry units

        public Ingredient()
        {
        }

        public void ReportLeftOvers(string name)
        {
            const int nameLength = -26;
            const int countLength = 4;
            Console.WriteLine($"{name, nameLength}:{PantryUnits, countLength} {PantryUnitDescription}");
        }
    }
}