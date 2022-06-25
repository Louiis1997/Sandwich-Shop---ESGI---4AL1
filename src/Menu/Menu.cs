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
}