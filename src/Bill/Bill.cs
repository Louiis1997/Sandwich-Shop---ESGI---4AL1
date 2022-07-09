namespace sandwichshop.Bill;

using System.Collections.Generic;
using Currencies;
using Quantity;
using Sandwich;

public class Bill : IBilling
{
    private readonly Dictionary<Sandwich, Quantity> _sandwiches;
    private double _totalPrice = 0;
    private Currency _totalPriceUnit;
    private readonly QuantityUnits _units;

    private string factureText =
        " ______         _\n" +
        "|  ____|       | |\n" +
        "| |__ __ _  ___| |_ _   _ _ __ ___\n" +
        "|  __/ _` |/ __| __| | | | '__/ _ \\\n" +
        "| | | (_| | (__| |_| |_| | | |  __/\n" +
        "|_|  \\__,_|\\___|\\__|\\__,_|_|  \\___|\n";


    public Bill(QuantityUnits units)
    {
        _sandwiches = new Dictionary<Sandwich, Quantity>();
        this._units = units;
    }

    public void AddSandwich(Sandwich sandwich, Quantity quantity)
    {
        if (sandwich == null) return;
        var newQuantity = _sandwiches.ContainsKey(sandwich)
            ? new Quantity(_sandwiches[sandwich].Value + quantity.Value, _sandwiches[sandwich].QuantityUnit)
            : quantity;
        _sandwiches.Add(sandwich, newQuantity);
    }

    public void AddUserCommand(Command.Command command)
    {
        foreach (KeyValuePair<Sandwich, int> sandwichWithQuantity in command.GetSandwiches())
        {
                _sandwiches.Add(sandwichWithQuantity.Key, new Quantity(sandwichWithQuantity.Value, _units.Get(QuantityUnitName.None)));
        }
    }

    public string Generate()
    {
        if (_sandwiches.Count == 0) return "Votre commande est vide.";
        var sandwichesInBill = "";
        foreach (var (sandwich, quantity) in _sandwiches)
        {
            sandwichesInBill += $"- {quantity} {sandwich.Name} à {sandwich.Price}\n";
            foreach (Ingredient sandwichIngredient in sandwich.Ingredients)
            {
                sandwichesInBill +=
                    $"\t{sandwichIngredient.Quantity} {sandwichIngredient.Name}\n";
            }
            
            _totalPrice += sandwich.Price.Value * quantity.Value;
            if (_totalPriceUnit == null)
            {
                _totalPriceUnit = sandwich.Price.Unit;
            }
        }

        return
            $"{factureText}\n" +
            sandwichesInBill +
            $"\nPrix total : {_totalPrice}{_totalPriceUnit?.Symbol}";
    }
}
