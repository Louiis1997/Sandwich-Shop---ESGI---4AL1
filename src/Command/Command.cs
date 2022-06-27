using System.Collections.Generic;

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
    public Dictionary<sandwichshop.Sandwich.Sandwich, int> ParseCommand(Menu.Menu menu, string userEntry)
    {
        userEntry = userEntry.Trim();
        string[] splitSandwich = userEntry.Split(", ");
        foreach (string sandwich in splitSandwich)
        {
            var splitQuantity = sandwich.Split(" ", 2);
            if (sandwiches.ContainsKey(menu.FindSandwich(splitQuantity[1])))
            {
                sandwiches[menu.FindSandwich(splitQuantity[1])] += int.Parse(splitQuantity[0]);
            }
            else
            {
                sandwiches.Add(menu.FindSandwich(splitQuantity[1]), int.Parse(splitQuantity[0]));
            }
        }
        return sandwiches;
    }
}