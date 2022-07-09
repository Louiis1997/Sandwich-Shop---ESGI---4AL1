#nullable enable
using System;
using System.Collections.Generic;
using sandwichshop.Exceptions;
using sandwichshop.Sandwiches;
using sandwichshop.Stock;

namespace sandwichshop.Shop;

public class Menu
{
    private readonly List<Sandwich> _availableSandwiches = new();

    public void AddSandwich(Sandwich sandwich)
    {
        _availableSandwiches.Add(sandwich);
    }

    public Sandwich FindSandwich(string name)
    {
        var found = _availableSandwiches.Find(s => s.Name.ToLower() == name.ToLower());
        if (found == null) throw new SandwichNotFoundException(name);
        return found;
    }

    public void DisplayMenu()
    {
        Console.Clear();
        Console.WriteLine("===========================================================");
        Console.WriteLine("Bienvenue dans la fameuse Sandwicherie !");
        Console.WriteLine("===========================================================");
        Console.WriteLine("Veuillez choisir un sandwich: ");
        foreach (var sandwich in _availableSandwiches)
        {
            Console.Write($"- {sandwich.Name} à {sandwich.Price} : ");
            for (var i = 0; i < sandwich.Ingredients.Count; i++)
            {
                var sandwichIngredient = sandwich.Ingredients[i];
                Console.Write($"{sandwichIngredient.Quantity} {sandwichIngredient.Name}" +
                              (i == sandwich.Ingredients.Count - 1 ? "\n" : ", "));
            }
        }
    }

    public void OrderSandwich(Sandwich sandwich, ShopStock shopStock)
    {
        var ingredients = sandwich.Ingredients;
        foreach (var ingredient in ingredients)
        {
            if (!shopStock.ContainsEnough(ingredient))
                throw new ArgumentException("Ingredient not available : " + ingredient + ".\nAvailable ingredients : " +
                                            shopStock.AvailableIngredients);
            shopStock.Use(ingredient);
        }
    }
}