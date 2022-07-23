using System;
using System.Collections.Generic;
using sandwichshop.Currencies;
using sandwichshop.Quantity;
using sandwichshop.Sandwiches;
using Xunit;

namespace sandwichshop.tests.Sandwiches;

public class SandwichCommandTest
{
    private string _name;
    private Price _price;
    private Ingredient _bread;
    private Ingredient _tuna;
    private Ingredient _tomato;
    private Ingredient _mayonnaise;
    private Ingredient _salad;
    private List<Ingredient> _ingredients;
    private Sandwich _dieppois;
    private List<Ingredient> _ingredientsAddedBread;
    private Price _priceAddedBread;
    private Sandwich _dieppoisAddedBread;
    private List<Ingredient> _ingredientsAddedBreadRemovedTuna;
    private Price _priceAddedBreadRemovedTuna;
    private Sandwich _dieppoisAddedBreadRemovedTuna;
    
    [Fact]
    public void Init()
    {
        _name = "Dieppois";
        _price = new (4.50, new Currency(CurrencyName.Euro, "€"));
        _bread = new (new Quantity.Quantity(1, new QuantityUnit(QuantityUnitName.None, "")),
        "pain");
        _tuna = new (new Quantity.Quantity(50, new QuantityUnit(QuantityUnitName.Gram, "g")),
        "de thon");
        _tomato = new (new Quantity.Quantity(0.5, new QuantityUnit(QuantityUnitName.None, "")),
        "tomate");
        _mayonnaise = new (new Quantity.Quantity(10, new QuantityUnit(QuantityUnitName.Gram, "g")),
        "de mayonnaise");
        _salad = new (new Quantity.Quantity(10, new QuantityUnit(QuantityUnitName.Gram, "g")),
        "de salade");
        _ingredients = new (new[] { _bread, _tuna, _tomato, _mayonnaise, _salad });
        _dieppois = new (_name, _ingredients, _price);
        _ingredientsAddedBread = new (new[] { _bread, _tuna, _tomato, _mayonnaise, _salad, _bread });
        _priceAddedBread = new (5.50, new Currency(CurrencyName.Euro, "€"));
        _dieppoisAddedBread = new(_name, _ingredientsAddedBread, _priceAddedBread);
        _ingredientsAddedBreadRemovedTuna = new (new[] { _bread, _tomato, _mayonnaise, _salad, _bread });
        _priceAddedBreadRemovedTuna = new (6.00, new Currency(CurrencyName.Euro, "€"));
        _dieppoisAddedBreadRemovedTuna = new(_name, _ingredientsAddedBreadRemovedTuna, _priceAddedBreadRemovedTuna);
    }

    [Fact] 
    public void Sandwich_avec_une_methode_Add_et_Remove()
    {
            Init();
            
            Sandwich sandwich = _dieppois;

            Assert.Equal(_dieppois, sandwich);

            sandwich.Add(_bread);

            Assert.Equal(_ingredientsAddedBread, sandwich.Ingredients);
            
            Assert.NotEqual(_dieppoisAddedBread.Price, sandwich.Price);

            sandwich.Remove(_tuna);

            Assert.Equal(_ingredientsAddedBreadRemovedTuna, sandwich.Ingredients);
            
            Assert.NotEqual(_dieppoisAddedBreadRemovedTuna.Price, sandwich.Price);
    }
    
    [Fact] 
    public void Remove_un_ingredient_non_present_dans_le_sandwich()
    {
        Init();
            
        Sandwich sandwich = _dieppois;
        
        Assert.Throws<ArgumentException>(() =>  sandwich.Remove(new (new Quantity.Quantity(1, new QuantityUnit(QuantityUnitName.None, "")),
            "tranche de jambon")));
    }


    [Fact] 
    public void AddCommand_avec_une_methode_Do_et_Undo()
    { 
        Init();
        
        Sandwich sandwich = _dieppois;
        
        AddCommand add = new AddCommand(sandwich, _bread);
        
        Assert.Equal(_ingredients, sandwich.Ingredients);
        
        add.Do();
        
        Assert.Equal(_ingredientsAddedBread, sandwich.Ingredients);
        
        Assert.Equal(_dieppoisAddedBread.Price, sandwich.Price);
        
        add.Undo();
        
        Assert.Equal(_ingredients, sandwich.Ingredients);
        
        Assert.Equal(_dieppois.Price, sandwich.Price);
    }
    
    [Fact] 
    public void RemoveCommand_avec_une_methode_Do_et_Undo() 
    { 
        Init();
        
        Sandwich sandwich = _dieppois;
        
        AddCommand add = new AddCommand(sandwich, _bread);
        
        RemoveCommand remove = new RemoveCommand(sandwich, _tuna);
        
        Assert.Equal(_ingredients, sandwich.Ingredients);
        
        add.Do();
        
        Assert.Equal(_ingredientsAddedBread, sandwich.Ingredients);
        
        Assert.Equal(_dieppoisAddedBread.Price, sandwich.Price);
        
        remove.Do();
        
        Assert.Equal(_ingredientsAddedBreadRemovedTuna, sandwich.Ingredients);
        
        Assert.Equal(_dieppoisAddedBreadRemovedTuna.Price, sandwich.Price);
        
        remove.Undo();
        
        Assert.Equivalent(_ingredientsAddedBread, sandwich.Ingredients);
        
        Assert.Equal(_dieppoisAddedBread.Price, sandwich.Price);
    }
    
    [Fact] 
    public void RemoveCommand_avec_un_ingredient_non_present_dans_le_sandwich() 
    { 
        Init();
        
        Sandwich sandwich = _dieppois;

        RemoveCommand remove = new RemoveCommand(sandwich, new (new Quantity.Quantity(1, new QuantityUnit(QuantityUnitName.None, "")),
            "tranche de jambon"));

        Assert.Throws<ArgumentException>(() => remove.Do());
    }
    
    [Fact] 
    public void SandwichCommand_avec_une_methode_Do_et_Undo_pour_unifier_AddCommand_et_RemoveCommand() 
    { 
        Init();
        
        Sandwich sandwich = _dieppois;
        
        SandwichCommand add = new AddCommand(sandwich, _bread);
        
        add.Do();
        
        Assert.Equal(_ingredientsAddedBread, sandwich.Ingredients);
        
        Assert.Equal(_dieppoisAddedBread.Price, sandwich.Price);
        
        SandwichCommand remove = new RemoveCommand(sandwich, _tuna);
        
        remove.Do();
        
        Assert.Equal(_ingredientsAddedBreadRemovedTuna, sandwich.Ingredients);
        
        Assert.Equal(_dieppoisAddedBreadRemovedTuna.Price, sandwich.Price);
        
        remove.Undo();
        
        Assert.Equivalent(_ingredientsAddedBread, sandwich.Ingredients);
        
        Assert.Equal(_dieppoisAddedBread.Price, sandwich.Price);
        
        add.Undo();
        
        Assert.Equal(_ingredients, sandwich.Ingredients);
        
        Assert.Equal(_dieppois.Price, sandwich.Price);
    }
    
    [Fact] 
    public void SandwichCommand_avec_un_RemoveCommand_avec_un_ingredient_non_present_dans_le_sandwich() 
    { 
        Init();
        
        Sandwich sandwich = _dieppois;
        
        SandwichCommand remove = new RemoveCommand(sandwich, new (new Quantity.Quantity(1, new QuantityUnit(QuantityUnitName.None, "")),
            "tranche de jambon"));
        
        Assert.Throws<ArgumentException>(() => remove.Do());
    }
}