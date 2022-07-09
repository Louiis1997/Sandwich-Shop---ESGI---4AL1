using System.Linq;
using sandwichshop.Sandwiches;
using sandwichshop.Stocks;

namespace sandwichshop.Stock;

public class ShopStock
{
    public readonly AvailableIngredients AvailableIngredients;

    public ShopStock(AvailableIngredients ingredients)
    {
        AvailableIngredients = ingredients;
    }

    public bool HasEnoughIngredientsForSandwich(Sandwich orderedSandwich)
    {
        var ingredientsInSandwich = orderedSandwich.Ingredients;
        // For each ingredient in sandwich
        return ingredientsInSandwich.All(ingredient => AvailableIngredients.ContainsEnough(ingredient));
    }

    public bool ContainsEnough(Ingredient ingredient)
    {
        return AvailableIngredients.ContainsEnough(ingredient);
    }

    public void Use(Ingredient ingredient)
    {
        AvailableIngredients.Use(ingredient);
    }
}