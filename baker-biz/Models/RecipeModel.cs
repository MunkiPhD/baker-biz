using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baker_biz.Models
{
    public class RecipeModel
    {
        public string Name { get; set; } = "Error: Name did not load!";
        public List<IngredientModel> Ingredients { get; set; } = new List<IngredientModel>();
    }
}
