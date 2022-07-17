using System;
using System.Collections.Generic;
using sandwichshop.Billing;
using sandwichshop.CLI;
using sandwichshop.Currencies;
using sandwichshop.Order;
using sandwichshop.Quantity;
using sandwichshop.Sandwiches;
using sandwichshop.Stock;
using sandwichshop.Stocks;

namespace sandwichshop.Shop;

// Façade pattern
public class SandwichShop
{
    private readonly Menu _menu;
    private readonly QuantityUnits _quantityUnits;
    private readonly ShopStock _shopStock;
    private SandwichFactory _sandwichFactory;
    private List<Ingredient> _ingredients;
    private IngredientFactory _ingredientFactory;

    private SandwichShop(Menu menu, ShopStock shopStock,
        QuantityUnits quantityUnits, SandwichFactory sandwichFactory, IngredientFactory ingredientFactory, List<Ingredient> ingredients)
    {
        _menu = menu;
        _shopStock = shopStock;
        _quantityUnits = quantityUnits;
        _sandwichFactory = sandwichFactory;
        _ingredients= ingredients;
        _ingredientFactory = ingredientFactory;
    }

    public static SandwichShop Initialize()
    {
        #region Currencies

        var currencies = new Currencies.Currencies();
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

        IngredientFactory ingredientFactory = new IngredientFactory(quantityUnits);
        var bread = ingredientFactory.CreateIngredient("pain");
        var ham = ingredientFactory.CreateIngredient("jambon");
        var butter = ingredientFactory.CreateIngredient("beurre");
        var egg = ingredientFactory.CreateIngredient("oeuf");
        var tomato = ingredientFactory.CreateIngredient("tomate");
        var sliceOfChicken = ingredientFactory.CreateIngredient("poulet");
        var mayonnaise = ingredientFactory.CreateIngredient("mayonnaise");
        var salad = ingredientFactory.CreateIngredient("salade");
        var thon = ingredientFactory.CreateIngredient("thon");

        List<Ingredient> ingredients = new List<Ingredient>();
        ingredients.Add(bread);
        ingredients.Add(ham);
        ingredients.Add(butter);
        ingredients.Add(egg);
        ingredients.Add(tomato);
        ingredients.Add(sliceOfChicken);
        ingredients.Add(mayonnaise);
        ingredients.Add(salad);
        ingredients.Add(thon);
        
        SandwichFactory sandwichFactory = new SandwichFactory(currencies, ingredients);
        var dieppois = sandwichFactory.CreateSandwich("Dieppois");
        var butterHamSandwich = sandwichFactory.CreateSandwich("Jambon beurre");
        var chickenVegetablesSandwich = sandwichFactory.CreateSandwich("Poulet crudités");
        
        #endregion

        #region Create Menu with all sandwiches and available ingredients

        var availableIngredients = new AvailableIngredients();
        availableIngredients.Restock(new Quantity.Quantity(3, bread.Quantity.QuantityUnit), bread);
        availableIngredients.Restock(new Quantity.Quantity(100, ham.Quantity.QuantityUnit), ham);
        availableIngredients.Restock(new Quantity.Quantity(100, butter.Quantity.QuantityUnit), butter);
        availableIngredients.Restock(new Quantity.Quantity(100, egg.Quantity.QuantityUnit), egg);
        availableIngredients.Restock(new Quantity.Quantity(1, tomato.Quantity.QuantityUnit), tomato);
        availableIngredients.Restock(new Quantity.Quantity(100, sliceOfChicken.Quantity.QuantityUnit), sliceOfChicken);
        availableIngredients.Restock(new Quantity.Quantity(100, mayonnaise.Quantity.QuantityUnit), mayonnaise);
        availableIngredients.Restock(new Quantity.Quantity(100, salad.Quantity.QuantityUnit), salad);
        availableIngredients.Restock(new Quantity.Quantity(100, thon.Quantity.QuantityUnit), thon);

        var shopStock = new ShopStock(availableIngredients);

        var menu = Singleton<Menu>.Instance;
        // menu.SetAvailableIngredients(availableIngredients);

        menu.AddSandwich(dieppois);
        menu.AddSandwich(butterHamSandwich);
        menu.AddSandwich(chickenVegetablesSandwich);

        #endregion

        return new SandwichShop(menu, shopStock, quantityUnits, sandwichFactory, ingredientFactory, ingredients);
    }

    public void OpenForCommand()
    {
        try
        {
            HandleClientCommand();
        }
        catch (Exception e)
        {
            ClientCli.DisplayException(e);
        }
    }

    private void HandleClientCommand()
    {
        while (true)
        {
            #region Display menu and instructions to client

            ClientCli.DisplayMenu(_menu);

            #endregion

            #region Retrieve client command (see 'Sujet initial projet.pdf)

            var userEntry = ClientCli.RetrieveClientEntry();
            if (userEntry == ClientCli.QuitString)
            {
                ClientCli.DisplaySeeYouNextTime();
                break;
            }

            #endregion

            try
            {
                #region Parse client entry (command) to list of sandwich (create Command model ?) + Handle parsing error from client entry
                
                var command = new UserOrder();
                var parsedCommandMessage = command.ParseCommand(_menu, _shopStock, _sandwichFactory, _ingredientFactory, _ingredients, userEntry);

                #endregion

                #region Display bill to client

                var bill = new Bill(_quantityUnits);
                bill.AddUserCommand(command);
                ClientCli.DisplayBill(bill, parsedCommandMessage);
                ClientCli.DisplayDoubleLineSeparation();

                #endregion
            }
            catch (Exception e)
            {
                ClientCli.DisplayUnexpectedCommandFormatError(e);
            }

            if (!ClientCli.AskUserWantsToReorder()) break;
        }
    }
}