using System.Collections.Generic;
using System.Linq;
using sandwichshop.Currencies;
using sandwichshop.Order;
using sandwichshop.Quantity;
using sandwichshop.Sandwiches;

namespace sandwichshop.Billing;

public class Bill : IBilling
{
    private readonly Dictionary<Sandwich, Quantity.Quantity> _sandwiches;
    private readonly QuantityUnits _units;

    private readonly string factureText =
        "\n========================================\n" +
        " ______         _\n" +
        "|  ____|       | |\n" +
        "| |__ __ _  ___| |_ _   _ _ __ ___\n" +
        "|  __/ _` |/ __| __| | | | '__/ _ \\\n" +
        "| | | (_| | (__| |_| |_| | | |  __/\n" +
        "|_|  \\__,_|\\___|\\__|\\__,_|_|  \\___|\n";

    private double _totalPrice;
    private Currency _totalPriceUnit;

    public Bill(QuantityUnits units)
    {
        _sandwiches = new Dictionary<Sandwich, Quantity.Quantity>();
        _units = units;
    }

    public string Generate(string parsedCommandMessage = "")
    {
        if (parsedCommandMessage.Length > 0) return $"{factureText}\n{parsedCommandMessage}";
        if (_sandwiches.Count == 0) return "Votre commande est vide.";

        var sandwichesInBill = "";
        foreach (var (sandwich, quantity) in _sandwiches)
        {
            sandwichesInBill += $"- {quantity} {sandwich.Name} à {sandwich.Price}\n";
            sandwichesInBill = sandwich.Ingredients.Aggregate(sandwichesInBill,
                (current, sandwichIngredient) =>
                    current + $"\t{sandwichIngredient.Quantity} {sandwichIngredient.Name}\n");

            _totalPrice += sandwich.Price.Value * quantity.Value;
            _totalPriceUnit ??= sandwich.Price.Unit;
        }


        return
            $"{factureText}\n" +
            sandwichesInBill +
            $"\nPrix total : {_totalPrice}{_totalPriceUnit?.Symbol}";
    }

    public void AddSandwich(Sandwich sandwich, Quantity.Quantity quantity)
    {
        if (sandwich == null) return;
        var newQuantity = _sandwiches.ContainsKey(sandwich)
            ? new Quantity.Quantity(_sandwiches[sandwich].Value + quantity.Value, _sandwiches[sandwich].QuantityUnit)
            : quantity;
        _sandwiches.Add(sandwich, newQuantity);
    }

    public void AddUserCommand(UserOrder userOrder)
    {
        foreach (var sandwichWithQuantity in userOrder.GetSandwiches())
            _sandwiches.Add(sandwichWithQuantity.Key,
                new Quantity.Quantity(sandwichWithQuantity.Value, _units.Get(QuantityUnitName.None)));
    }
}