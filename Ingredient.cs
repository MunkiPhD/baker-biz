using System.Text.Json;

namespace bakerbiz
{
    public class Ingredient
    {
        public int Amount { get; set; }                 // units used by a recipe
        public int UnitMultiplier { get; set; } = 1;    // recipe units per pantry unit
        public double PantryUnits                       // Units input to the pantry and reported at the end
        {
            get => Convert.ToDouble(Amount) / Convert.ToDouble(UnitMultiplier);
            set => Amount = Convert.ToInt16(value * UnitMultiplier); 
        }
        public string? Units { get; set; }              // Name for the pantry units

        public Ingredient()
        {
        }

        public void ReportLeftOvers(string name)
        {
            const int nameLength = -26;
            const int countLength = 4;
            Console.WriteLine($"{name, nameLength}:{PantryUnits, countLength} {Units}");
        }
    }
}