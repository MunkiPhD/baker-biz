namespace bakerbiz
{
    public class Pantry
    {
        Dictionary<Ingredient_Type, Ingredient> supplies = new Dictionary<Ingredient_Type, Ingredient>();

        public Pantry(Dictionary<Ingredient_Type, Ingredient> ingrdnts)
        {
                supplies = ingrdnts;

                
        }

        public void GatherIngredients()
        {
            foreach (var i in supplies)
            {
                int cnt = 0;
                Console.WriteLine(i.Value.GatherMessage);
                if(!int.TryParse(Console.ReadLine(), out cnt))
                {
                    Console.WriteLine($"Please enter a number, looks like we're gonna use 0 for {i.Value.Name}!");
                    continue;
                }
                supplies[i.Key].AmountRemaining = cnt;
            }
        }

        public double GetAmmountRemaining(Ingredient_Type ingredient_Type)
        {
            return supplies[ingredient_Type].AmountRemaining;
        }

        public void UseIngredient(Ingredient_Type ingredient_Type, double amount)
        {
            supplies[ingredient_Type].AmountRemaining -= amount;
        }

        public void ReportLeftOvers()
        {
            const int nameLength = -26;
            const int countLength = 4;
            Console.WriteLine("You will have the following left over ingredients: ");
            foreach(var s in supplies)
            {
                Console.WriteLine($"{s.Value.Name, nameLength}:{s.Value.AmountRemaining, countLength}");
            }
        }
    }
}