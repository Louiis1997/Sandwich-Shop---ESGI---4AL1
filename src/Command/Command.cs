namespace sandwichshop.Command;

using System;
using System.Collections.Generic;
using Exceptions;
using Sandwich;
using Menu;

public class Command
{
    private Dictionary<Sandwich, int> sandwiches;

    public Command()
    {
        this.sandwiches = new Dictionary<Sandwich, int>();
    }

    public Dictionary<Sandwich, int> GetSandwiches()
    {
        return sandwiches;
    }
    public void ParseCommand(Menu menu, string userEntry)
    {
        userEntry = userEntry.Trim();
        string[] splitCommandItems = userEntry.Split(", ");
        foreach (string sandwich in splitCommandItems)
        {
            var splitQuantityAndSandwich = sandwich.Split(" ", 2);
            if (splitQuantityAndSandwich.Length != 2)
            {
                throw new ArgumentException("ATTENTION ! Votre commande doit être au format : <quantité1> <nom du sandwich>, <quantité2> <nom du sandwich>");
            }
            
            var quantity = int.Parse(splitQuantityAndSandwich[0]);
            
            try
            {
                var orderedSandwich = menu.FindSandwich(splitQuantityAndSandwich[1]);
                for (var i = 0; i < quantity; i++)
                {
                    if (!menu.HasEnoughIngredientsForSandwich(orderedSandwich))
                    {
                        Console.WriteLine("Désolé nous n'avons pas assez d'ingrédients pour faire un {0}", orderedSandwich.Name);
                        continue;
                    }
                    AddSandwich(orderedSandwich);
                    menu.OrderSandwich(orderedSandwich);
                }
            }
            catch (SandwichNotFoundException e)
            {
                Console.WriteLine("===========================================================");
                Console.WriteLine(e.ClientMessageForCli);
                Console.WriteLine("===========================================================");
            }
        }
    }

    private void AddSandwich(Sandwich sandwich)
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