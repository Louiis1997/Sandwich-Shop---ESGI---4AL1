using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace sandwichshop.Sandwiches;
public class Sandwich
{
    public string Name { get; set; }
    public List<Ingredient> Ingredients { get; set; }
    public Price Price { get; set; }

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
    
    public void Add(Ingredient ingredient) => Ingredients.Add(ingredient);

    public void Remove(Ingredient ingredient)
    {
        if (Ingredients.Contains(ingredient))
        {
            Ingredients.Remove(ingredient);
        }
        else
        {
            throw new ArgumentException("Le sandwich ne contient pas cet ingredient");
        }
    }
}