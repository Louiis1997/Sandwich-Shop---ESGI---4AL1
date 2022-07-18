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
    private readonly Stack<SandwichCommand> _history;

    public UserOrder()
    {
        _sandwiches = new Dictionary<Sandwich, int>();
        _notEnoughtStockForSandwiches = new Dictionary<Sandwich, int>();
        _history = new Stack<SandwichCommand>();
    }

    public Dictionary<Sandwich, int> GetSandwiches()
    {
        return _sandwiches;
    }

    public string ParseCommand(Menu menu, ShopStock stock, SandwichFactory sandwichFactory, IngredientFactory ingredientFactory ,List<Ingredient> ingredients, string userEntry)
    {
        userEntry = userEntry.Trim();
        var splitCommandItems = userEntry.Split(", ");
        foreach (var sandwich in splitCommandItems)
        {
            var splitSandwichAndPersonalization = sandwich.Split(" : ");
            var splitQuantityAndSandwich = splitSandwichAndPersonalization[0].Split(" ", 2);
            if (splitQuantityAndSandwich.Length != 2)
                throw new ArgumentException(
                    "/!\\ ATTENTION ! Votre commande doit être au format : <quantité1> <nom du sandwich>, <quantité2> <nom du sandwich>");
            var quantity = int.Parse(splitQuantityAndSandwich[0]);

            try
            {
                var orderedSandwich = sandwichFactory.CreateSandwich(splitQuantityAndSandwich[1]);
                if (splitSandwichAndPersonalization.Length > 1)
                {
                    var splitPersonalization = splitSandwichAndPersonalization[1].Split(" ");
                    if (splitPersonalization.Length < 2 || splitPersonalization.Length % 2 != 0)
                    {
                        throw new ArgumentException(
                            "/!\\ ATTENTION ! Votre commande doit être au format : <quantité1> <nom du sandwich> : <signe '+' ou '-'> <ingredient>, <quantité2> <nom du sandwich> : <signe '+' ou '-'> <ingredient>");

                    }
                    else if (splitPersonalization.Length >= 2 && splitPersonalization.Length % 2 == 0)
                    {
                        for (int i = 0; i < splitPersonalization.Length; i += 2)
                        {
                            Modify(splitPersonalization[i], orderedSandwich,
                                ingredients.Find(ingredient => ingredientFactory.CreateIngredient(splitPersonalization[i + 1]).Name == ingredient.Name));
                        }
                    }
                }
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
    
    public void Modify(string op, Sandwich sandwich, Ingredient ingredient)
    {
        SandwichCommand cmd = op switch
        {
            "+" => new AddCommand(sandwich, ingredient),
            "-" => new RemoveCommand(sandwich, ingredient),
            _ => throw new ArgumentException(
                "/!\\ ATTENTION ! Votre commande doit être au format : <quantité1> <nom du sandwich> : <signe '+' ou '-'> <ingredient>, <quantité2> <nom du sandwich> : <signe '+' ou '-'> <ingredient>")
        };
        cmd.Do();
        _history.Push(cmd);
    }
    
    public void Undo()
    {
        var cmd = _history.Pop();
        cmd.Undo();
    }
}