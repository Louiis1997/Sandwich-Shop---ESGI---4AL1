using System;
using System.Collections.Generic;
using sandwichshop.Currencies;
using sandwichshop.Exceptions;
using sandwichshop.Quantity;
using sandwichshop.Sandwiches;
using Xunit;

namespace sandwichshop.tests;

public class SandwichCreationTest
{
    private static string name = "Dieppois";
    private static Price price = new (4.50, new Currency(CurrencyName.Euro, "€"));
    private static Ingredient bread = new (new Quantity.Quantity(1, new QuantityUnit(QuantityUnitName.None, "")),
        "pain");
    private static Ingredient tuna = new (new Quantity.Quantity(50, new QuantityUnit(QuantityUnitName.Gram, "g")),
        "de thon");
    private static Ingredient tomato = new (new Quantity.Quantity(0.5, new QuantityUnit(QuantityUnitName.None, "")),
        "tomate");
    private static Ingredient mayonnaise = new (new Quantity.Quantity(10, new QuantityUnit(QuantityUnitName.Gram, "g")),
        "de mayonnaise");

    private static Ingredient salad = new(new Quantity.Quantity(10, new QuantityUnit(QuantityUnitName.Gram, "g")),
        "de salade");
    private static List<Ingredient> ingredients = new (new [] {bread, tuna, tomato, mayonnaise, salad});
    private Sandwich dieppois = new (name, ingredients, price);
    
    [Fact]
    public void Creer_une_classe_SandwichBuilder()
    {
        SandwichBuilder sandwichBuilder = new SandwichBuilder();
            
        Assert.NotNull(sandwichBuilder);
    }

    [Fact]
    public void Creer_une_methode_pour_stocker_le_nom_d_un_sandwich()
    {
        SandwichBuilder sandwichBuilder = new SandwichBuilder();
            
        sandwichBuilder = sandwichBuilder
            .WithName("dieppois");

        Assert.NotNull(sandwichBuilder);
    }

    [Fact]
    public void Initialiser_un_sandwich_avec_un_nom_seulement()
    {
        SandwichBuilder sandwichBuilder = new SandwichBuilder();

        Sandwich sandwich;

        Assert.Throws<SandwichBuilderException>(() => sandwichBuilder.WithName("dieppois").Build());
    }

    [Fact]
    public void Creer_un_sandwich_dieppois()
    {
        SandwichBuilder sandwichBuilder = new SandwichBuilder();

        Sandwich sandwich =
            sandwichBuilder
                .WithName("Dieppois")
                .WithPrice(new Price(4.50, new Currency(CurrencyName.Euro, "€")))
                .WithIngredient(new Ingredient(new Quantity.Quantity(1, new QuantityUnit(QuantityUnitName.None, "")),
                    "pain"))
                .WithIngredient(new Ingredient(new Quantity.Quantity(50, new QuantityUnit(QuantityUnitName.Gram, "g")),
                    "de thon"))
                .WithIngredient(new Ingredient(new Quantity.Quantity(0.5, new QuantityUnit(QuantityUnitName.None, "")),
                    "tomate"))
                .WithIngredient(new Ingredient(new Quantity.Quantity(10, new QuantityUnit(QuantityUnitName.Gram, "g")),
                    "de mayonnaise"))
                .WithIngredient(new Ingredient(new Quantity.Quantity(10, new QuantityUnit(QuantityUnitName.Gram, "g")),
                    "de salade"))
                .Build();

        Assert.Equal(name, sandwich.Name);
        Assert.Equal(price, sandwich.Price);
        Assert.Equal(ingredients, sandwich.Ingredients);
    }
    
    [Fact]
    public void Creer_une_classe_SandwichFactory_avec_une_methode_Create()
    {
        #region Currencies
        var currencies = new Currencies.Currencies();
        currencies.Add(CurrencyName.Euro, "€");
        currencies.Add(CurrencyName.Dollar, "$");
        currencies.Add(CurrencyName.Pound, "£");
        currencies.Add(CurrencyName.Yen, "¥");
        #endregion
        
        SandwichFactory sandwichFactory = new SandwichFactory(currencies, ingredients);
        Sandwich sandwich = sandwichFactory.CreateSandwich("dieppois");
        Assert.Equal(dieppois, sandwich);
    }
    
    [Fact]
    public void Creer_un_sandwich_inexistant_avec_SandwichFactory_avec_la_methode_Create()
    {
        #region Currencies
        var currencies = new Currencies.Currencies();
        currencies.Add(CurrencyName.Euro, "€");
        currencies.Add(CurrencyName.Dollar, "$");
        currencies.Add(CurrencyName.Pound, "£");
        currencies.Add(CurrencyName.Yen, "¥");
        #endregion
        
        SandwichFactory sandwichFactory = new SandwichFactory(currencies, ingredients);
        Assert.Throws<ArgumentException>(() => sandwichFactory.CreateSandwich("Hot Dog"));
    }
}