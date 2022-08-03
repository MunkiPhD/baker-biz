using System;

namespace BakerBiz
{
    public interface IPieRecipe
    {
        IList<Ingredient> Ingredients { get; }
        string Name { get; }

    }
}