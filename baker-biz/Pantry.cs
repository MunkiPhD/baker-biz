using System.Text.Json;
using System.Text.Json.Serialization;

namespace bakerbiz
{
    public class Pantry
    {
        Dictionary<string, Ingredient> supplies = new Dictionary<string, Ingredient>();

        public Pantry(string ingrdnts)
        {
                string jsonString = File.ReadAllText(ingrdnts);
                var options = new JsonSerializerOptions
                {
                    Converters =
                    {
                        new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
                    }
                };
                supplies = JsonSerializer.Deserialize<Dictionary<string, Ingredient>>(jsonString, options) ?? new Dictionary<string, Ingredient>();
        }

        public int GetAmountRemaining(string ingredient_Name)
        {
            return supplies[ingredient_Name].Amount;
        }

        public void UseIngredient(string ingredient_Name, int amount)
        {
            supplies[ingredient_Name].Amount -= amount;
        }

        public void ReportLeftOvers()
        {
            Console.WriteLine("You will have the following left over ingredients: ");
            foreach(var s in supplies)
            {
                s.Value.ReportLeftOvers(s.Key);
            }
        }
    }
}