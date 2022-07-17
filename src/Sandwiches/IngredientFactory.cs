using System;
using sandwichshop.Quantity;

namespace sandwichshop.Sandwiches;

public class IngredientFactory
{
    private QuantityUnits quantityUnits;
    
    public IngredientFactory(QuantityUnits quantityUnits)
    {
        this.quantityUnits = quantityUnits;
    }

    public Ingredient CreateIngredient(string ingredient)
    {
        return ingredient.ToLower() switch
        {
            "pain" => new Ingredient(new Quantity.Quantity(1, quantityUnits.Get(QuantityUnitName.None)), 
                "pain"),
            "jambon" => new Ingredient(new Quantity.Quantity(1, quantityUnits.Get(QuantityUnitName.None)), 
                "tranche de jambon"),
            "beurre" => new Ingredient(new Quantity.Quantity(10, quantityUnits.Get(QuantityUnitName.Gram)), 
                "de beurre"),
            "oeuf" => new Ingredient(new Quantity.Quantity(1, quantityUnits.Get(QuantityUnitName.None)), 
                "oeuf"), 
            "tomate" => new Ingredient(new Quantity.Quantity(0.5, quantityUnits.Get(QuantityUnitName.None)), 
                "tomate"),
            "poulet" => new Ingredient(new Quantity.Quantity(1, quantityUnits.Get(QuantityUnitName.None)), 
                "tranche de poulet"),
            "mayonnaise" => new Ingredient(new Quantity.Quantity(10, quantityUnits.Get(QuantityUnitName.Gram)), 
                "de mayonnaise"),
            "salade" => new Ingredient(new Quantity.Quantity(10, quantityUnits.Get(QuantityUnitName.Gram)), 
                "de salade"),
            "thon" => new Ingredient(new Quantity.Quantity(50, quantityUnits.Get(QuantityUnitName.Gram)), 
                "de thon"),
            _ => throw new ArgumentException("Cet ingredient n'existe pas") 
        };
    }
}