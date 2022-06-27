using System;
using System.Collections.Generic;
using sandwichshop.Currencies;
using sandwichshop.Bill;
using sandwichshop.Command;
using sandwichshop.Menu;
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
dieppoisIngredients.Add(bread);
dieppoisIngredients.Add(thon);
dieppoisIngredients.Add(tomato);
dieppoisIngredients.Add(mayonnaise);
dieppoisIngredients.Add(salad);

Sandwich dieppois = new Sandwich("Dieppois", dieppoisIngredients, new Price(4.50, currencies.Get(CurrencyName.Euro)));
Sandwich butterHamSandwich = new Sandwich("Jambon beurre", butterHam, new Price(3.50, currencies.Get(CurrencyName.Euro)));
Sandwich chickenVegetablesSandwich = new Sandwich(
    "Poulet crudités",
    chickenVegetablesSandwichIngredients,
    new Price(5, currencies.Get(CurrencyName.Euro))
);

#endregion

#region Create Menu with all sandwiches

Menu menu = new Menu();
menu.AddSandwich(dieppois);
menu.AddSandwich(butterHamSandwich);
menu.AddSandwich(chickenVegetablesSandwich);

#endregion

#region Display menu and instructions to client
menu.DisplayMenu();
#endregion

#region Retrieve client command (see 'Sujet initial projet.pdf)

 string userEntry = Console.ReadLine();

#endregion

#region Parse client entry (command) to list of sandwich (create Command model ?) + Handle parsing error from client entry

Command command = new Command();
command.parseCommand(menu, userEntry);

#endregion

/* Extract from 'Sujet initial projet.pdf :
 " 2.4 Comportement attendu du programme
        Votre programme devra récupérer l'entrée de l'utilisateur et valider sa conformité.
        En cas de commande incorrecte, votre programme produira une erreur compréhensible mais ne devra pas crasher.
        En cas de commande correcte, votre programme écrira dans la console la facture.
        Après avoir traité une commande, votre programme attendra la commande suivante, il ne doit pas s'arrêter après avoir écrit une facture.
    "
 */

#region Generate bill from sandwiches
// TODO
#endregion

#region Display bill to client
// TODO
#endregion

// TODO : remove next line (they are examples to create a bill and generate it in console)
Bill bill = new Bill(quantityUnits);
bill.AddUserCommand(command);
Console.WriteLine(bill.Generate());