using System;
using System.Collections.Generic;
using sandwichshop.Sandwich;

namespace sandwichshop.Command;

public class Command
{
    private Dictionary<sandwichshop.Sandwich.Sandwich, int> sandwiches;

    public Command()
    {
        this.sandwiches = new Dictionary<sandwichshop.Sandwich.Sandwich, int>();
    }

    public Dictionary<sandwichshop.Sandwich.Sandwich, int> GetSandwiches()
    {
        return sandwiches;
    }
    public void ParseCommand(Menu.Menu menu, string userEntry)
    {
        userEntry = userEntry.Trim();
        string[] splitCommandItems = userEntry.Split(", ");
        foreach (string sandwich in splitCommandItems)
        {
            var splitQuantityAndSandwich = sandwich.Split(" ", 2);
            var orderedSandwich = menu.FindSandwich(splitQuantityAndSandwich[1]);
            var quantity = int.Parse(splitQuantityAndSandwich[0]);
            
            for (var i = 0; i < quantity; i++)
            {
                if (!menu.HasEnoughIngredientsForSandwich(quantity, orderedSandwich))
                {
                    Console.WriteLine("Désolé nous n'avons pas assez d'ingrédients pour faire un {0}", orderedSandwich.Name);
                    continue;
                }
                AddSandwich(orderedSandwich);
                menu.OrderSandwich(orderedSandwich);
            }
        }
    }

    private void AddSandwich(Sandwich.Sandwich sandwich)
    {
        if (sandwiches.ContainsKey(sandwich))
        {
            sandwiches[sandwich] += 1;
        }
        else
        {
            sandwiches.Add(sandwich, 1);
        }
    }
}