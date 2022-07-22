using System;
using sandwichshop.Billing;
using sandwichshop.CLI;
using sandwichshop.Order;
using sandwichshop.Shop;

namespace sandwichshop.OrderMethod;

public class TextOrder: IOrderMethod
{
    public override void Order(SandwichShop sandwichShop)
    {
        
        #region Retrieve client all command from commandsFolder/commands.txt()

        //Read each line of the file into a string array. 
        string commandPath = "../../../commandsFolder/commands.txt";
        string[] commands = System.IO.File.ReadAllLines(commandPath);
    
        #endregion

        foreach (var userEntry in commands)
        {
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
}