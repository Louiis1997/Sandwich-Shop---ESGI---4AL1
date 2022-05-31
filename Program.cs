using System;
using System.Collections.Generic;
using sandwichshop.Currencies;
using sandwichshop.Bill;
using sandwichshop.Quantity;
using sandwichshop.Sandwich;
using sandwichshop.Sandwich;

#region Currencies

Currencies currencies = new Currencies();
currencies.Add(CurrencyName.Euro, "€");
currencies.Add(CurrencyName.Dollar, "$");
currencies.Add(CurrencyName.Pound, "£");
currencies.Add(CurrencyName.Yen, "¥");

#endregion

#region Quantity Units

QuantityUnits quantityUnits = new QuantityUnits();
quantityUnits.Add(QuantityUnitName.Gram, "g");
quantityUnits.Add(QuantityUnitName.Milligram, "mg");
quantityUnits.Add(QuantityUnitName.None, "");

#endregion

#region Sandwiches

Ingredient bread = new Ingredient(new Quantity(1, quantityUnits.Get(QuantityUnitName.None)), "pain");
Ingredient ham = new Ingredient(new Quantity(1, quantityUnits.Get(QuantityUnitName.None)), "tranche de jambon");
Ingredient butter = new Ingredient(new Quantity(10, quantityUnits.Get(QuantityUnitName.Gram)), "de beurre");

Ingredient egg = new Ingredient(new Quantity(1, quantityUnits.Get(QuantityUnitName.None)), "oeuf");
Ingredient tomato = new Ingredient(new Quantity(0.5, quantityUnits.Get(QuantityUnitName.None)), "tomate");
Ingredient sliceOfChicken = new Ingredient(new Quantity(1, quantityUnits.Get(QuantityUnitName.None)), "tranche de poulet");
Ingredient mayonnaise = new Ingredient(new Quantity(10, quantityUnits.Get(QuantityUnitName.Gram)), "de mayonnaise");
Ingredient salad = new Ingredient(new Quantity(10, quantityUnits.Get(QuantityUnitName.Gram)), "de salade");

Ingredient thon = new Ingredient(new Quantity(50, quantityUnits.Get(QuantityUnitName.Gram)), "de thon");

List<Ingredient> butterHam = new List<Ingredient>();
butterHam.Add(bread);
butterHam.Add(ham);
butterHam.Add(butter);

List<Ingredient> chickenVegetablesSandwichIngredients = new List<Ingredient>();
chickenVegetablesSandwichIngredients.Add(bread);
chickenVegetablesSandwichIngredients.Add(egg);
chickenVegetablesSandwichIngredients.Add(tomato);
chickenVegetablesSandwichIngredients.Add(sliceOfChicken);
chickenVegetablesSandwichIngredients.Add(mayonnaise);
chickenVegetablesSandwichIngredients.Add(salad);

List<Ingredient> dieppoisIngredients = new List<Ingredient>();
chickenVegetablesSandwichIngredients.Add(bread);
chickenVegetablesSandwichIngredients.Add(thon);
chickenVegetablesSandwichIngredients.Add(tomato);
chickenVegetablesSandwichIngredients.Add(mayonnaise);
chickenVegetablesSandwichIngredients.Add(salad);

Sandwich dieppois = new Sandwich("Dieppois", dieppoisIngredients, new Price(4.50, currencies.Get(CurrencyName.Euro)));
Sandwich butterHamSandwich = new Sandwich("Jambon beurre", butterHam, new Price(3.50, currencies.Get(CurrencyName.Euro)));
Sandwich chickenVegetablesSandwich = new Sandwich(
    "Poulet crudités",
    chickenVegetablesSandwichIngredients,
    new Price(5, currencies.Get(CurrencyName.Euro))
);

#endregion

Bill bill = new Bill();
bill.AddSandwich(butterHamSandwich, new Quantity(1, quantityUnits.Get(QuantityUnitName.None)));
bill.AddSandwich(dieppois, new Quantity(1, quantityUnits.Get(QuantityUnitName.None)));
bill.AddSandwich(chickenVegetablesSandwich, new Quantity(1, quantityUnits.Get(QuantityUnitName.None)));
Console.WriteLine(bill.Generate());