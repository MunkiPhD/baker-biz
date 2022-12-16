using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baker_biz_interfaces
{
    public interface IUserInterface
    {
        public IRecipeController? SelectRecipe(IEnumerable<IRecipeController> recipes);
        public void ReportCalculationResults(IRecipeController recipe);
    }
}
