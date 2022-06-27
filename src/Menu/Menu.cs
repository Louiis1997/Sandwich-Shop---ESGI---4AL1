#nullable enable
using System.Collections.Generic;

namespace sandwichshop.Menu;
using Sandwich;

public class Menu
{
    private List<Sandwich> AvailableSandwiches;
    
    public Menu(List<Sandwich>? sandwiches = null)
    {
        AvailableSandwiches = sandwiches ?? new List<Sandwich>();
    }
    
    public void AddSandwich(Sandwich sandwich)
    {
        AvailableSandwiches.Add(sandwich);
    }
    
    public void RemoveSandwich(Sandwich sandwich)
    {
        AvailableSandwiches.Remove(sandwich);
    }
    
    public Sandwich FindSandwich(string name)
    {
        Sandwich? found = AvailableSandwiches.Find(s => s.Name.ToLower() == name.ToLower());
        if (found == null)
        {
            throw new System.Exception("Sandwich not found with name : " + name);
        }
        return found;
    }
    
    public void DisplayMenu()
    {
        System.Console.WriteLine("\n\n\n===================================");
        System.Console.WriteLine("Bienvenue dans le sandwich shop");
        System.Console.WriteLine("Veuillez choisir un sandwich: ");
        foreach (Sandwich sandwich in AvailableSandwiches)
        {
            System.Console.Write($"- {sandwich.Name} à {sandwich.Price} : ");
            for (int i = 0; i < sandwich.Ingredients.Count; i++)
            {
                Ingredient sandwichIngredient = sandwich.Ingredients[i];
                System.Console.Write($"{sandwichIngredient.Quantity} {sandwichIngredient.Name}" +
                                     (i == sandwich.Ingredients.Count - 1 ? "\n" : ", "));
            }
        }
    }
}