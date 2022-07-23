using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace sandwichshop.Sandwiches;

[Serializable]
[XmlInclude(typeof(Ingredient))]
[XmlInclude(typeof(Price))]
public class Sandwich
{
    public Sandwich(string name, List<Ingredient> ingredients, Price price)
    {
        Name = name;
        Ingredients = ingredients;
        Price = price;
    }

    // Default constructor required for XML serialization
    public Sandwich()
    {
    }

    public string Name { get; set; }
    public List<Ingredient> Ingredients { get; }
    public Price Price { get; set; }

    // to string
    public override string ToString()
    {
        return $"{Name}";
    }

    public void Add(Ingredient ingredient)
    {
        Ingredients.Add(ingredient);
    }

    public void Remove(Ingredient ingredient)
    {
        if (Ingredients.Contains(ingredient))
            Ingredients.Remove(ingredient);
        else
            throw new ArgumentException("Le sandwich ne contient pas cet ingredient");
    }
    
    public override bool Equals(object obj)
    {
        if (obj == null)
            return false;

        if (obj.GetType() != GetType())
            return false;

        var sandwich = (Sandwich)obj;
        return Name == sandwich.Name;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Ingredients, Price);
    }
}