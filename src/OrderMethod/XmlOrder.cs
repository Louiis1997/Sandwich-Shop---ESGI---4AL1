using System;
using sandwichshop.Billing;
using sandwichshop.CLI;
using sandwichshop.Command;
using sandwichshop.Order;
using sandwichshop.Shop;

namespace sandwichshop.OrderMethod;

public class XmlOrder: IOrderMethod
{
    public override void Order(SandwichShop sandwichShop)
    {
        #region Retrieve client all command from commandsFolder/commands.xml
        
        string commandPath = "../../../commandsFolder/commands.xml";

        XmlConverter<Commands> converter = new XmlConverter<Commands>();
        Commands commands = converter.Deserialize(commandPath);

        foreach (var command in commands.CommandList) 
        {
            var userEntry = CommandUtils.CommandToUserEntry(command.Command);
             
            try
            {
                #region Parse client entry (command) to list of sandwich (create Command model ?) + Handle parsing error from client entry
                 
                var userOrder = new UserOrder();
                var parsedCommandMessage = userOrder.ParseCommand(sandwichShop.Menu, sandwichShop.ShopStock, sandwichShop.SandwichFactory, sandwichShop.IngredientFactory, sandwichShop.Ingredients, userEntry);

                #endregion

                #region Display bill to client

                var bill = new Bill(sandwichShop.QuantityUnits);
                bill.AddUserCommand(userOrder);
                SelectBillingMethod(bill, parsedCommandMessage);

                #endregion
            }
            catch (Exception e)
            {
                ClientCli.DisplayUnexpectedCommandFormatError(e);
            } 
        }
        #endregion
    }
}