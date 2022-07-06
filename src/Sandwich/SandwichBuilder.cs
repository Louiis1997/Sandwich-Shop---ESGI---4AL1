using System;
using System.Collections.Generic;
using sandwichshop.Currencies;
using sandwichshop.Exceptions;

namespace sandwichshop.Sandwich;

public class SandwichBuilder
{
    private String Name;
    private List<Ingredient> Ingredients;
    private Price Price;
    
    public SandwichBuilder()
    {
        Name = "";
        Ingredients = new();
        Price = null;
    }
    
    public SandwichBuilder WithName(string Name) {
        this.Name = Name;
        return this;
    }
    
    public SandwichBuilder WithPrice(Price price) {
        this.Price = price;
        return this;
    }
    
    public SandwichBuilder WithIngredient(Ingredient ingredient) {
        Ingredients.Add(ingredient);
        return this;
    }

    public Sandwich Build() {
        if (Name == "" || Price == null || Ingredients.Count == 0) {
            throw new SandwichBuilderException("Invalid sandwich : missing name, price or ingredients");
        }
        Sandwich sandwich = new Sandwich(Name, Ingredients, Price);
        Reset();
        return sandwich;
    }
    
    public void Reset()
    {
        Name = "";
        Ingredients = new();
        Price = null;
    }
}