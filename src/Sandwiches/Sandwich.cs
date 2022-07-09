using System.Collections.Generic;

namespace sandwichshop.Sandwiches;

public class Sandwich
{
    public readonly List<Ingredient> Ingredients;
    public readonly string Name;
    public readonly Price Price;

    public Sandwich(string name, List<Ingredient> ingredients, Price price)
    {
        Name = name;
        Ingredients = ingredients;
        Price = price;
    }

    // to string
    public override string ToString()
    {
        return $"{Name}";
    }
}