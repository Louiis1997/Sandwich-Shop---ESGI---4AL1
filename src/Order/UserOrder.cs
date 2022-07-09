using System;
using System.Collections.Generic;
using sandwichshop.Exceptions;
using sandwichshop.Sandwiches;
using sandwichshop.Shop;
using sandwichshop.Stock;

namespace sandwichshop.Order;

public class UserOrder
{
    private readonly Dictionary<Sandwich, int> _notEnoughtStockForSandwiches;
    private Dictionary<Sandwich, int> _sandwiches;

    public UserOrder()
    {
        _sandwiches = new Dictionary<Sandwich, int>();
        _notEnoughtStockForSandwiches = new Dictionary<Sandwich, int>();
    }

    public Dictionary<Sandwich, int> GetSandwiches()
    {
        return _sandwiches;
    }

    public string ParseCommand(Menu menu, ShopStock stock, string userEntry)
    {
        userEntry = userEntry.Trim();
        var splitCommandItems = userEntry.Split(", ");
        foreach (var sandwich in splitCommandItems)
        {
            var splitQuantityAndSandwich = sandwich.Split(" ", 2);
            if (splitQuantityAndSandwich.Length != 2)
                throw new ArgumentException(
                    "/!\\ ATTENTION ! Votre commande doit être au format : <quantité1> <nom du sandwich>, <quantité2> <nom du sandwich>");

            var quantity = int.Parse(splitQuantityAndSandwich[0]);

            try
            {
                var orderedSandwich = menu.FindSandwich(splitQuantityAndSandwich[1]);
                for (var i = 0; i < quantity; i++)
                {
                    if (!stock.HasEnoughIngredientsForSandwich(orderedSandwich))
                    {
                        // Add if not already in the list
                        if (!_notEnoughtStockForSandwiches.ContainsKey(orderedSandwich))
                            _notEnoughtStockForSandwiches.Add(orderedSandwich, 1);
                        else
                            _notEnoughtStockForSandwiches[orderedSandwich]++;
                        continue;
                    }

                    AddSandwich(orderedSandwich);
                    menu.OrderSandwich(orderedSandwich, stock);
                }
            }
            catch (SandwichNotFoundException e)
            {
                Console.WriteLine("===========================================================");
                Console.WriteLine(e.ClientMessageForCli);
                Console.WriteLine("===========================================================");
            }
        }

        if (_notEnoughtStockForSandwiches.Count > 0) return CannotOrderSandwiches();

        return "";
    }

    private void AddSandwich(Sandwich sandwich)
    {
        if (_sandwiches.ContainsKey(sandwich))
            _sandwiches[sandwich] += 1;
        else
            _sandwiches.Add(sandwich, 1);
    }

    private string CannotOrderSandwiches()
    {
        _sandwiches = new Dictionary<Sandwich, int>();
        var message =
            "/!\\ ATTENTION ! Nous n'avons pas pu effectuer votre commande.\nIl n'y a pas assez de stock pour les sandwiches suivants :\n";
        foreach (var sandwich in _notEnoughtStockForSandwiches) message += $"{sandwich.Value} {sandwich.Key}" + ", ";

        message = message.Substring(0, message.Length - 2);
        return message;
    }
}