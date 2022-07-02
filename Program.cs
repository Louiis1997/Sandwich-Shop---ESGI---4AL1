using System;
using sandwichshop.Currencies;
using sandwichshop.Bill;
using sandwichshop.Command;
using sandwichshop.Exceptions;
using sandwichshop.Menu;
using sandwichshop.Quantity;
using sandwichshop.Sandwich;

#region Currencies

try {
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

    SandwichBuilder sandwichBuilder = new SandwichBuilder();

    Sandwich dieppois = sandwichBuilder
        .WithName("Dieppois")
        .WithPrice(new Price(4.50, currencies.Get(CurrencyName.Euro)))
        .WithIngredient(bread)
        .WithIngredient(thon)
        .WithIngredient(tomato)
        .WithIngredient(mayonnaise)
        .WithIngredient(salad)
        .Build();

    Sandwich butterHamSandwich = sandwichBuilder
        .WithName("Jambon beurre")
        .WithPrice(new Price(3.50, currencies.Get(CurrencyName.Euro)))
        .WithIngredient(bread)
        .WithIngredient(ham)
        .WithIngredient(butter)
        .Build();

    Sandwich chickenVegetablesSandwich = sandwichBuilder
        .WithName("Poulet crudités")
        .WithPrice(new Price(5, currencies.Get(CurrencyName.Euro)))
        .WithIngredient(bread)
        .WithIngredient(egg)
        .WithIngredient(tomato)
        .WithIngredient(sliceOfChicken)
        .WithIngredient(mayonnaise)
        .WithIngredient(salad)
        .Build();

    #endregion

    #region Create Menu with all sandwiches and available ingredients
    AvailableIngredients availableIngredients = new AvailableIngredients();
    availableIngredients.Restock(new Quantity(3, bread.Quantity.QuantityUnit), bread);
    availableIngredients.Restock(new Quantity(100, ham.Quantity.QuantityUnit), ham);
    availableIngredients.Restock(new Quantity(100, butter.Quantity.QuantityUnit), butter);
    availableIngredients.Restock(new Quantity(100, egg.Quantity.QuantityUnit), egg);
    availableIngredients.Restock(new Quantity(1, tomato.Quantity.QuantityUnit), tomato);
    availableIngredients.Restock(new Quantity(100, sliceOfChicken.Quantity.QuantityUnit), sliceOfChicken);
    availableIngredients.Restock(new Quantity(100, mayonnaise.Quantity.QuantityUnit), mayonnaise);
    availableIngredients.Restock(new Quantity(100, salad.Quantity.QuantityUnit), salad);
    availableIngredients.Restock(new Quantity(100, thon.Quantity.QuantityUnit), thon);
    
    Menu menu = Singleton<Menu>.Instance;;
    menu.SetAvailableIngredients(availableIngredients);
    
    menu.AddSandwich(dieppois);
    menu.AddSandwich(butterHamSandwich);
    menu.AddSandwich(chickenVegetablesSandwich);
    #endregion

    while (true)
    {
        #region Display menu and instructions to client
        menu.DisplayMenu();
        #endregion

        #region Retrieve client command (see 'Sujet initial projet.pdf)

        string userEntry = Console.ReadLine();
        Console.WriteLine();

        #endregion

        try
        {
            #region Parse client entry (command) to list of sandwich (create Command model ?) + Handle parsing error from client entry

            Command command = new Command();
            command.ParseCommand(menu, userEntry);

            #endregion

            #region Display bill to client

            Bill bill = new Bill(quantityUnits);
            bill.AddUserCommand(command);
            Console.WriteLine(bill.Generate());
            Console.WriteLine("==========================================================");

            #endregion
        }
        catch (Exception e)
        {
            Console.WriteLine("==========================================================");
            Console.WriteLine("Votre commande ne correspond pas au format attendu :");
            Console.WriteLine(e);
            Console.WriteLine("==========================================================\n");
        }

        Console.WriteLine("Voulez-vous faire une autre commande ? O/n");
        string endProgramOrContinue = Console.ReadLine();
        if (endProgramOrContinue != null && endProgramOrContinue.ToLower() == "n")
        {
            Console.WriteLine("====================================================");
            Console.WriteLine("À la prochaine !");
            Console.WriteLine("====================================================");
            break;
        }
    }
} catch (Exception e) {
    Console.WriteLine(e.Message);
}

// TODO : Fixer les problèmes d'import qui fait qu'on doit écrire par exemple 'Sandwich.Sandwich'
// TODO : Mieux gérer le fait qu'un sandwich n'existe pas
// TODO : Refacto avec des design patterns
// TODO : Revoir les responsabilités des classes
// TODO : Voir s'il faut plus de services (Command readline, etc.)
