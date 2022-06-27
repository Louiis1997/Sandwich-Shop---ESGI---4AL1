using System.Collections.Generic;
using sandwichshop.Currencies;
using sandwichshop.Quantity;
using sandwichshop.Sandwich;

namespace sandwichshop.Bill
{
    public class Bill : IBilling
    {
        private Dictionary<sandwichshop.Sandwich.Sandwich, Quantity.Quantity> sandwiches;
        private double TotalPrice = 0;
        private Currency TotalPriceUnit;
        private QuantityUnits Units;

        private string factureText =
            " ______         _\n" +
            "|  ____|       | |\n" +
            "| |__ __ _  ___| |_ _   _ _ __ ___\n" +
            "|  __/ _` |/ __| __| | | | '__/ _ \\\n" +
            "| | | (_| | (__| |_| |_| | | |  __/\n" +
            "|_|  \\__,_|\\___|\\__|\\__,_|_|  \\___|\n";


        public Bill(QuantityUnits Units)
        {
            sandwiches = new Dictionary<sandwichshop.Sandwich.Sandwich, Quantity.Quantity>();
            this.Units = Units;
        }

        public void AddSandwich(sandwichshop.Sandwich.Sandwich sandwich, Quantity.Quantity quantity)
        {
            if (sandwich == null) return;
            var newQuantity = sandwiches.ContainsKey(sandwich)
                ? new Quantity.Quantity(sandwiches[sandwich].Value + quantity.Value, sandwiches[sandwich].QuantityUnit)
                : quantity;
            sandwiches.Add(sandwich, newQuantity);
        }

        public void AddUserCommand(Command.Command command)
        {
            foreach (KeyValuePair<Sandwich.Sandwich, int> sandwichWithQuantity in command.GetSandwiches())
            {
                    this.sandwiches.Add(sandwichWithQuantity.Key, new Quantity.Quantity(sandwichWithQuantity.Value, Units.Get(QuantityUnitName.None)));
            }
        }

        public string Generate()
        {
            string sandwichesInBill = "";
            foreach (var (sandwich, quantity) in sandwiches)
            {
                sandwichesInBill += $"- {quantity} {sandwich.Name} à {sandwich.Price}\n";
                foreach (Ingredient sandwichIngredient in sandwich.Ingredients)
                {
                    sandwichesInBill +=
                        $"\t{sandwichIngredient.Quantity} {sandwichIngredient.Name}\n";
                }
                
                TotalPrice += sandwich.Price.Value * quantity.Value;
                if (TotalPriceUnit == null)
                {
                    TotalPriceUnit = sandwich.Price.Unit;
                }
            }

            return
                $"{factureText}\n" +
                sandwichesInBill +
                $"\nPrix total : {TotalPrice}{TotalPriceUnit.Symbol}";
        }
    }
}