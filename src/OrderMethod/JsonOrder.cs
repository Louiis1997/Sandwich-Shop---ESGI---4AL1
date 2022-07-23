using System.Collections.Generic;
using sandwichshop.Command;
using sandwichshop.Shop;

namespace sandwichshop.OrderMethod;

public class JsonOrder: IOrderMethod
{
    public override List<string> Order(SandwichShop sandwichShop)
    {
        #region Return client all command commandsFolder/commands.json

        string commandPath = "../../../commandsFolder/commands.json";
        
        var commands = JsonConverter<Commands>.Deserialize(commandPath);
        List<string> commandsString = new List<string>();
        foreach (var command in commands.CommandList)
        {
            commandsString.Add(CommandUtils.CommandToUserEntry(command.Command));
        }
        return commandsString;
        
        #endregion
    }
}