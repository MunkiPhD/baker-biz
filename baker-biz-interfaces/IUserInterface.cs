using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baker_biz_interfaces
{
    public interface IUserInterface
    {
        public IRecipe? SelectRecipe(IEnumerable<IRecipe> recipes);
        public void ReportCalculationResults(IRecipe recipe);
    }
}
