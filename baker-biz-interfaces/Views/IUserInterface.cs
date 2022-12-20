using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using baker_biz_interfaces.Controllers;

namespace baker_biz_interfaces.Views
{
    public interface IUserInterface
    {
        public IRecipeController? SelectRecipe(IEnumerable<IRecipeController> recipes);
        public void ReportCalculationResults(IRecipeController recipe);
    }
}
