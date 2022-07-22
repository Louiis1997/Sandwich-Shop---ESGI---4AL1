using System;
using sandwichshop.Billing;
using sandwichshop.CLI;
using sandwichshop.Order;
using sandwichshop.Shop;

namespace sandwichshop.OrderMethod;

public class CliOrder: IOrderMethod
{
    public override void Order(SandwichShop sandwichShop)
    {
        #region Display menu and instructions to client

        ClientCli.DisplayMenu(sandwichShop.Menu);

        #endregion
        
        #region Retrieve client command (see 'Sujet initial projet.pdf)

        var userEntry = ClientCli.RetrieveClientCliEntry();

        #endregion
        
        try
        {
            #region Parse client entry (command) to list of sandwich (create Command model ?) + Handle parsing error from client entry
            
            var command = new UserOrder();
            var parsedCommandMessage = command.ParseCommand(sandwichShop.Menu, sandwichShop.ShopStock, sandwichShop.SandwichFactory, sandwichShop.IngredientFactory, sandwichShop.Ingredients, userEntry);

            #endregion

            #region Display bill to client

            var bill = new Bill(sandwichShop.QuantityUnits);
            bill.AddUserCommand(command);
            SelectBillingMethod(bill, parsedCommandMessage);

            #endregion
        }
        catch (Exception e)
        {
            ClientCli.DisplayUnexpectedCommandFormatError(e);
        }
    }
}