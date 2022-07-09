namespace sandwichshop.Sandwich;

using System;
using System.Collections.Generic;

public class Sandwich
{ 
    public readonly String Name;
    public readonly List<Ingredient> Ingredients; 
    public readonly Price Price;

    public Sandwich(String name, List<Ingredient> ingredients, Price price)
    {
        Name = name;
        Ingredients = ingredients;
        Price = price;
    }
}
