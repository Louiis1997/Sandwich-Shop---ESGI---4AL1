using System.Collections.Generic;
using sandwichshop.Sandwiches;

namespace sandwichshop.Command;

public class CommandUtils
{
    public static string CommandToUserEntry(IList<OrderedSandwich> command)
    {
        var userEntry = "";
        for (int i = 0; i < command.Count; i++)
        {
            userEntry += " "+command[i].ToString();
            if (i < command.Count - 1)
            {
                userEntry += ",";
            }
        }
        return userEntry;
    }
}