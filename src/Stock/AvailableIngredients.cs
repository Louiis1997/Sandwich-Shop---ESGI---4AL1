using System.Collections.Generic;
using sandwichshop.Sandwich;

namespace sandwichshop.Stocks;

public class AvailableIngredients
{
    private Dictionary<Ingredient, Quantity.Quantity> availableIngredients;

    public AvailableIngredients(Dictionary<Ingredient, Quantity.Quantity> availableIngredients)
    {
        this.availableIngredients = availableIngredients;
    }
    
    public AvailableIngredients()
    {
        this.availableIngredients = new Dictionary<Ingredient, Quantity.Quantity>();
    }
    
    public void Restock(Quantity.Quantity quantity, Ingredient ingredient)
    {
        if (availableIngredients.ContainsKey(ingredient))
        {
            Quantity.Quantity alreadyAvailableQuantity = availableIngredients[ingredient];
            if (!Equals(alreadyAvailableQuantity.QuantityUnit, quantity.QuantityUnit))
            {
                throw new System.Exception("Cannot restock with different quantity unit");
            }
            availableIngredients[ingredient].Value += quantity.Value;
        }
        else
        {
            availableIngredients.Add(ingredient, quantity);
        }
    }
    
    public void Use(Ingredient ingredient)
    {
        if (availableIngredients.ContainsKey(ingredient))
        {
            Quantity.Quantity alreadyAvailableQuantity = availableIngredients[ingredient];
            if (!Equals(alreadyAvailableQuantity.QuantityUnit, ingredient.Quantity.QuantityUnit))
            {
                throw new System.Exception("Cannot use with different quantity unit : " + ingredient.Quantity.QuantityUnit + " != " + alreadyAvailableQuantity.QuantityUnit);
            }
            if (alreadyAvailableQuantity.Value < ingredient.Quantity.Value || alreadyAvailableQuantity.Value <= 0)
            {
                throw new System.Exception("Not enough ingredients available : " + ingredient.Name);
            }
            availableIngredients[ingredient].Value -= ingredient.Quantity.Value;
        }
        else
        {
            throw new System.Exception("Cannot use ingredient that is not available");
        }
    }

    public bool ContainsEnough(Ingredient ingredient)
    {
        if (availableIngredients.ContainsKey(ingredient))
        {
            return availableIngredients[ingredient].Value > 0;
        }
        return false;
    }

    public override string ToString()
    {
        string result = "\n";
        foreach (KeyValuePair<Ingredient, Quantity.Quantity> ingredient in availableIngredients)
        {
            result += "\t- " + ingredient.Key.Name + " : " + ingredient.Value + "\n";
        }
        return result;
    }
}