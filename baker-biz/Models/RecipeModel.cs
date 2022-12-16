using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baker_biz.Models
{
    internal class RecipeModel
    {
        public string Name { get; set; } = "Error: Name did not load!";
        public Dictionary<string, uint> Supplies { get; set; } = new Dictionary<string, uint>();
        public List<IngredientModel> Ingredients { get; set; } = new List<IngredientModel>();
    }
}
