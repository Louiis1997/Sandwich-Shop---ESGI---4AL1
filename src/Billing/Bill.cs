using System.Collections.Generic;
using System.Linq;
using sandwichshop.Currencies;
using sandwichshop.Order;
using sandwichshop.Quantity;
using sandwichshop.Sandwiches;

namespace sandwichshop.Billing;

public class Bill : IBilling
{
    public Dictionary<Sandwich, Quantity.Quantity> Sandwiches { get; }
    private readonly QuantityUnits _units;

    private readonly string factureText =
        "\n========================================\n" +
        " ______         _\n" +
        "|  ____|       | |\n" +
        "| |__ __ _  ___| |_ _   _ _ __ ___\n" +
        "|  __/ _` |/ __| __| | | | '__/ _ \\\n" +
        "| | | (_| | (__| |_| |_| | | |  __/\n" +
        "|_|  \\__,_|\\___|\\__|\\__,_|_|  \\___|\n";

    public double TotalPrice { get; set; }
    private Currency _totalPriceUnit;

    public Bill(QuantityUnits units)
    {
        Sandwiches = new Dictionary<Sandwich, Quantity.Quantity>();
        _units = units;
    }

    public string Generate(string parsedCommandMessage = "")
    {
        if (parsedCommandMessage.Length > 0) return $"{factureText}\n{parsedCommandMessage}";
        if (Sandwiches.Count == 0) return "Votre commande est vide.";

        var sandwichesInBill = "";
        foreach (var (sandwich, quantity) in Sandwiches)
        {
            sandwichesInBill += $"- {quantity} {sandwich.Name} à {sandwich.Price}\n";
            sandwichesInBill = sandwich.Ingredients.Aggregate(sandwichesInBill,
                (current, sandwichIngredient) =>
                    current + $"\t{sandwichIngredient.Quantity} {sandwichIngredient.Name}\n");

            TotalPrice += sandwich.Price.Value * quantity.Value;
            _totalPriceUnit ??= sandwich.Price.Unit;
        }


        return
            $"{factureText}\n" +
            sandwichesInBill +
            $"\nPrix total : {TotalPrice}{_totalPriceUnit?.Symbol}";
    }

    public void AddSandwich(Sandwich sandwich, Quantity.Quantity quantity)
    {
        if (sandwich == null) return;
        var newQuantity = Sandwiches.ContainsKey(sandwich)
            ? new Quantity.Quantity(Sandwiches[sandwich].Value + quantity.Value, Sandwiches[sandwich].QuantityUnit)
            : quantity;
        Sandwiches.Add(sandwich, newQuantity);
    }

    public void AddUserCommand(UserOrder userOrder)
    {
        foreach (var sandwichWithQuantity in userOrder.GetSandwiches())
            Sandwiches.Add(sandwichWithQuantity.Key,
                new Quantity.Quantity(sandwichWithQuantity.Value, _units.Get(QuantityUnitName.None)));
    }
}