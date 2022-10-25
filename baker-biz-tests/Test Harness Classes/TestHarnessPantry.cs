using bakerbiz;
using baker_biz_interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baker_biz_tests
{
    internal class TestHarnessPantry : Pantry, IPantry
    {
        public TestHarnessPantry(Dictionary<string, Ingredient> supplies)
        {
            this.supplies = supplies;
        }

        public Dictionary<string, Ingredient> GetSupplies()
        {
            return supplies;
        }
    }
}
