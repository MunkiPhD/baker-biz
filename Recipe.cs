namespace bakerbiz
{
    public class Recipe
    {
        private string pieName = "";
        private Dictionary<Ingredient_Type, double> ingredientsRequired = new Dictionary<Ingredient_Type, double>();
        private int maxPies = Int16.MaxValue;

        public Recipe(string pname, Dictionary<Ingredient_Type, double> ingrdnts)
        {
            pieName = pname;
            ingredientsRequired = ingrdnts;
        }

        public void Calc(Pantry pantry)
        {
            CalcPieCounts(pantry);

            CalcLeftOvers(pantry);
        }

        private void CalcPieCounts(Pantry pantry)
        {
            foreach(var i in ingredientsRequired)
            {
                int piesFromIngredient = Convert.ToInt16(pantry.GetAmmountRemaining(i.Key) / i.Value);
                maxPies = Math.Min(maxPies, piesFromIngredient);
            }
        }

        private void CalcLeftOvers(Pantry pantry)
        {
            foreach(var i in ingredientsRequired)
            {
                pantry.UseIngredient(i.Key, (maxPies * i.Value));
            }
        }

        public void Report()
        {
            Console.WriteLine($"You can make {maxPies} {pieName} pies.");
        }
    }
}