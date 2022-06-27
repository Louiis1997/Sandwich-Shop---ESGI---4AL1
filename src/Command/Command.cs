using System.Collections.Generic;

namespace sandwichshop.Command;

public class Command
{
    private Dictionary<sandwichshop.Sandwich.Sandwich, int> sandwiches;

    public Command()
    {
        this.sandwiches = new Dictionary<sandwichshop.Sandwich.Sandwich, int>();
    }

    public Dictionary<sandwichshop.Sandwich.Sandwich, int> getSandwiches()
    {
        return sandwiches;
    }
    public Dictionary<sandwichshop.Sandwich.Sandwich, int> parseCommand(Menu.Menu menu, string userEntry)
    {
        userEntry = userEntry.Trim();
        string[] spliteSandwich = userEntry.Split(", ");
        string[] spliteQuantity;
        foreach (string sandwich in spliteSandwich)
        {
            spliteQuantity = sandwich.Split(" ", 2);
            sandwiches.Add(menu.FindSandwich(spliteQuantity[1]), int.Parse(spliteQuantity[0]));
        }
        return sandwiches;
    }
}