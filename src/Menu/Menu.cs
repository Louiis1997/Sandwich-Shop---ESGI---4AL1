#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using sandwichshop.Exceptions;

namespace sandwichshop.Menu;
using Sandwich;

public class Menu
{
    // Store sandwich with quantity
    private AvailableIngredients _availableIngredients;
    private List<Sandwich> _availableSandwiches = new();

    public Menu()
    {
        _availableIngredients = new AvailableIngredients();
    }
    
    public void SetAvailableIngredients(AvailableIngredients availableIngredients)
    {
        _availableIngredients = availableIngredients;
    }

    public void AddSandwich(Sandwich sandwich)
    {
        _availableSandwiches.Add(sandwich);
    }
    
    public Sandwich FindSandwich(string name)
    {
        Sandwich? found = _availableSandwiches.Find(s => s.Name.ToLower() == name.ToLower());
        if (found == null)
        {
            throw new SandwichNotFoundException(name);
        }
        return found;
    }
    
    public void DisplayMenu()
    {
        Console.WriteLine("\n\n\n===========================================================");
        Console.WriteLine("Bienvenue dans le sandwich shop");
        Console.WriteLine("Veuillez choisir un sandwich: ");
        foreach (Sandwich sandwich in _availableSandwiches)
        {
            Console.Write($"- {sandwich.Name} à {sandwich.Price} : ");
            for (var i = 0; i < sandwich.Ingredients.Count; i++)
            {
                Ingredient sandwichIngredient = sandwich.Ingredients[i];
                Console.Write($"{sandwichIngredient.Quantity} {sandwichIngredient.Name}" +
                                     (i == sandwich.Ingredients.Count - 1 ? "\n" : ", "));
            }
        }
    }
    
    public void OrderSandwich(Sandwich sandwich)
    {
        var ingredients = sandwich.Ingredients;
        foreach (var ingredient in ingredients)
        {
            if (!_availableIngredients.ContainsEnough(ingredient)) {
                throw new ArgumentException("Ingredient not available : " + ingredient + ".\nAvailable ingredients : " + _availableIngredients);
            }
            _availableIngredients.Use(ingredient);
        }
    }

    public bool HasEnoughIngredientsForSandwich(Sandwich orderedSandwich)
    {
        var ingredientsInSandwich = orderedSandwich.Ingredients;
        // For each ingredient in sandwich
        return ingredientsInSandwich.All(ingredient => _availableIngredients.ContainsEnough(ingredient));
    }
}