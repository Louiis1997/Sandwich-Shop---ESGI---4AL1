using System.Collections.Generic;
using sandwichshop.Command;
using sandwichshop.Shop;

namespace sandwichshop.OrderMethod;

public class XmlOrder: IOrderMethod
{
    public override List<string> Order(SandwichShop sandwichShop)
    {
        #region Retrieve client all command from commandsFolder/commands.xml
        
        string commandPath = "../../../commandsFolder/commands.xml";

        XmlConverter<Commands> converter = new XmlConverter<Commands>();
        Commands commands = converter.Deserialize(commandPath);
        
        List<string> commandsString = new List<string>();
        foreach (var command in commands.CommandList)
        {
            commandsString.Add(CommandUtils.CommandToUserEntry(command.Command));
        }
        return commandsString;
        
        #endregion
    }
}