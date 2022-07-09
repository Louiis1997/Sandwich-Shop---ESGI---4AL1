using System;
using sandwichshop.CLI;

namespace sandwichshop.Shop;

using Stocks;
using Menu;
using Currencies;
using Quantity;
using Sandwich;
using Bill;
using Command;

// Façade pattern
public class SandwichShop
{
    private Menu _menu;
    private ShopStock _shopStock;
    private Currencies _currencies;
    private QuantityUnits _quantityUnits;
    
    private SandwichShop(Menu menu, ShopStock shopStock, Currencies currencies, QuantityUnits quantityUnits)
    {
        _menu = menu;
        _shopStock = shopStock;
        _currencies = currencies;
        _quantityUnits = quantityUnits;
    }
    
    public static SandwichShop Initialize()
    {
        var shopStock = new ShopStock();

        #region Currencies
        var currencies = new Currencies();
        currencies.Add(CurrencyName.Euro, "€");
        currencies.Add(CurrencyName.Dollar, "$");
        currencies.Add(CurrencyName.Pound, "£");
        currencies.Add(CurrencyName.Yen, "¥");
        #endregion 
        
        #region Quantity Units
        var quantityUnits = new QuantityUnits();
        quantityUnits.Add(QuantityUnitName.Gram, "g");
        quantityUnits.Add(QuantityUnitName.Milligram, "mg");
        quantityUnits.Add(QuantityUnitName.None, "");
        #endregion
        
        #region Sandwiches
        var bread = new Ingredient(new Quantity(1, quantityUnits.Get(QuantityUnitName.None)), "pain");
        var ham = new Ingredient(new Quantity(1, quantityUnits.Get(QuantityUnitName.None)), "tranche de jambon");
        var butter = new Ingredient(new Quantity(10, quantityUnits.Get(QuantityUnitName.Gram)), "de beurre");

        var egg = new Ingredient(new Quantity(1, quantityUnits.Get(QuantityUnitName.None)), "oeuf");
        var tomato = new Ingredient(new Quantity(0.5, quantityUnits.Get(QuantityUnitName.None)), "tomate");
        var sliceOfChicken = new Ingredient(new Quantity(1, quantityUnits.Get(QuantityUnitName.None)), "tranche de poulet");
        var mayonnaise = new Ingredient(new Quantity(10, quantityUnits.Get(QuantityUnitName.Gram)), "de mayonnaise");
        var salad = new Ingredient(new Quantity(10, quantityUnits.Get(QuantityUnitName.Gram)), "de salade");

        var thon = new Ingredient(new Quantity(50, quantityUnits.Get(QuantityUnitName.Gram)), "de thon");

        var sandwichBuilder = new SandwichBuilder();

        var dieppois = sandwichBuilder
            .WithName("Dieppois")
            .WithPrice(new Price(4.50, currencies.Get(CurrencyName.Euro)))
            .WithIngredient(bread)
            .WithIngredient(thon)
            .WithIngredient(tomato)
            .WithIngredient(mayonnaise)
            .WithIngredient(salad)
            .Build();

        var butterHamSandwich = sandwichBuilder
            .WithName("Jambon beurre")
            .WithPrice(new Price(3.50, currencies.Get(CurrencyName.Euro)))
            .WithIngredient(bread)
            .WithIngredient(ham)
            .WithIngredient(butter)
            .Build();

        var chickenVegetablesSandwich = sandwichBuilder
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
    
        var menu = Singleton<Menu>.Instance;
        menu.SetAvailableIngredients(availableIngredients);
    
        menu.AddSandwich(dieppois);
        menu.AddSandwich(butterHamSandwich);
        menu.AddSandwich(chickenVegetablesSandwich);
        #endregion
        
        return new SandwichShop(menu, shopStock, currencies, quantityUnits);
    }
    
    public void OpenForCommand()
    {
        try {
            RetrieveClientCommand();
        } catch (Exception e)
        {
            ClientCli.DisplayException(e);
        }
    }

    private void RetrieveClientCommand()
    {
        
        while (true)
        {
            #region Display menu and instructions to client
            ClientCli.DisplayMenu(_menu);
            #endregion

            #region Retrieve client command (see 'Sujet initial projet.pdf)
            string userEntry = ClientCli.RetrieveClientEntry();
            if (userEntry == ClientCli.QUIT_STRING)
            {
                ClientCli.DisplaySeeYouNextTime();
                break;
            }
            #endregion

            try
            {
                #region Parse client entry (command) to list of sandwich (create Command model ?) + Handle parsing error from client entry

                Command command = new Command();
                command.ParseCommand(_menu, userEntry);

                #endregion

                #region Display bill to client

                Bill bill = new Bill(_quantityUnits);
                bill.AddUserCommand(command);
                ClientCli.DisplayBill(bill);
                ClientCli.DisplayDoubleLineSeparation();

                #endregion
            }
            catch (Exception e)
            {
                ClientCli.DisplayUnexpectedCommandFormatError(e);
            }

            if (!ClientCli.AskUserWantsToReorder())
            {
                break;
            }
        }
    }
}