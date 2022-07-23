using System.Collections.Generic;
using sandwichshop.CLI;
using sandwichshop.Shop;

namespace sandwichshop.OrderMethod;

public class CliOrder: IOrderMethod
{
    public override List<string> Order(SandwichShop sandwichShop)
    {
        #region Display menu and instructions to client

        ClientCli.DisplayMenu(sandwichShop.Menu);

        #endregion
        
        #region Retrieve client command (see 'Sujet initial projet.pdf)

        var userEntry = ClientCli.RetrieveClientCliEntry();

        List<string> commandsString = new List<string>();
        commandsString.Add(userEntry);
        return commandsString;
        
        #endregion
    }
}