using System;
using sandwichshop.Quantity;
using sandwichshop.Sandwiches;
using Xunit;

namespace sandwichshop.tests;

public class IngredientCreationTest
{
    private static Ingredient bread = new (new Quantity.Quantity(1, new QuantityUnit(QuantityUnitName.None, "")),
        "pain");
    private static Ingredient tuna = new (new Quantity.Quantity(50, new QuantityUnit(QuantityUnitName.Gram, "g")),
        "de thon");

    [Fact]
    public void Creer_une_classe_IngredientFactory_avec_une_methode_Create()
    {
        #region Quantity Units
        var quantityUnits = new QuantityUnits();
        quantityUnits.Add(QuantityUnitName.Gram, "g");
        quantityUnits.Add(QuantityUnitName.Milligram, "mg");
        quantityUnits.Add(QuantityUnitName.None, "");
        #endregion
        
        IngredientFactory ingredientFactory = new IngredientFactory(quantityUnits);
        Ingredient pain = ingredientFactory.CreateIngredient("pain");
        Assert.Equal(bread, pain);
    }
    
    [Fact]
    public void Creer_un_ingredient_inexistant_avec_IngredientFactory_avec_la_methode_Create()
    {
        #region Quantity Units
        var quantityUnits = new QuantityUnits();
        quantityUnits.Add(QuantityUnitName.Gram, "g");
        quantityUnits.Add(QuantityUnitName.Milligram, "mg");
        quantityUnits.Add(QuantityUnitName.None, "");
        #endregion
        
        IngredientFactory ingredientFactory = new IngredientFactory(quantityUnits);
        Assert.Throws<ArgumentException>(() => ingredientFactory.CreateIngredient("chocolat"));
    }
}