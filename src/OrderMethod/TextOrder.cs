using System.Collections.Generic;
using sandwichshop.Shop;

namespace sandwichshop.OrderMethod;

public class TextOrder: IOrderMethod
{
    public override List<string> Order(SandwichShop sandwichShop)
    {
        
        #region Retrieve client all command from commandsFolder/commands.txt()

        //Read each line of the file into a string array. 
        string commandPath = "../../../commandsFolder/commands.txt";
        string[] commands = System.IO.File.ReadAllLines(commandPath);
    
        List<string> commandsString = new List<string>();
        foreach (var command in commands)
        {
            commandsString.Add(command);
        }
        return commandsString;
        
        #endregion
    }
}