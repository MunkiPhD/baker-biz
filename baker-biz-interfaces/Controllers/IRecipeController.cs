using baker_biz_interfaces.Models;

namespace baker_biz_interfaces.Controllers
{
    public interface IRecipeController
    {
        Dictionary<string, uint> ProcessRecipe();
        Dictionary<string, IIngredientModel> GetLeftovers();
    }
}