using System.Collections.Generic;
using sandwichshop.Exceptions;

namespace sandwichshop.Sandwiches;

public class SandwichBuilder
{
    private List<Ingredient> Ingredients;
    private string Name;
    private Price Price;

    public SandwichBuilder()
    {
        Name = "";
        Ingredients = new List<Ingredient>();
        Price = null;
    }

    public SandwichBuilder WithName(string Name)
    {
        this.Name = Name;
        return this;
    }

    public SandwichBuilder WithPrice(Price price)
    {
        Price = price;
        return this;
    }

    public SandwichBuilder WithIngredient(Ingredient ingredient)
    {
        Ingredients.Add(ingredient);
        return this;
    }

    public Sandwich Build()
    {
        if (Name == "" || Price == null || Ingredients.Count == 0)
            throw new SandwichBuilderException("Invalid sandwich : missing name, price or ingredients");
        var sandwich = new Sandwich(Name, Ingredients, Price);
        Reset();
        return sandwich;
    }

    public void Reset()
    {
        Name = "";
        Ingredients = new List<Ingredient>();
        Price = null;
    }
}