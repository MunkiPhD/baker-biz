using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baker_biz_interfaces.Models
{
    public interface IIngredientModel
    {
        public string Name { get; set; }                    // Name for this IngredientModel
        public uint UnitConversionConstant { get; set; }    // recipe units per pantry unit
        public string PantryUnits { get; set; }             // Name for units input to the pantry and reported at the end
        public uint AmountRequired { get; set; }                  // Amount of this ingredient required
        public uint Supply { get; set; }                    // Volume of each ingredient, stored in recipe units
        public bool Optional { get; set; }                  // If this is an optional ingredient
        public double PantrySupply { get; set; }            // Getter and Setter for converting between pantry and recipe units
    }
}
