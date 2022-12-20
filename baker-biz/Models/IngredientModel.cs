using System.Text.Json;
using baker_biz_interfaces.Models;

namespace baker_biz.Models
{
    public class IngredientModel : IIngredientModel
    {
        public string Name { get; set; } = "";                  // Name for this IngredientModel
        public uint UnitConversionConstant { get; set; } = 1;   // recipe units per pantry unit
        public string PantryUnits { get; set; } = "";           // Name for units input to the pantry and reported at the end
        public uint AmountRequired { get; set; } = 0;           // Amount of this ingredient required
        public uint Supply { get; set; } = 0;                   // Volume of each ingredient, stored in recipe units
        public bool Optional { get; set; } = false;             // If this is an optional ingredient
        public double PantrySupply                              // Getter and Setter for converting between pantry and recipe units
        {
            get => Convert.ToDouble(Supply) / Convert.ToDouble(UnitConversionConstant);
            set => Supply = Convert.ToUInt16(value * UnitConversionConstant);
        }
    }
}