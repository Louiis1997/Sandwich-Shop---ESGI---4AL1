using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using sandwichshop.Sandwiches;

namespace sandwichshop.Command;

public class CommandUtils
{
    public static string CommandToUserEntry(List<OrderedSandwich> command)
    {
        var userEntry = "";
        for (int i = 0; i < command.Count; i++)
        {
            userEntry += " " + command[i];
            if (i < command.Count - 1)
            {
                userEntry += ",";
            }
        }
        return userEntry;
    }
}

public class OrderedSandwich
{
    [XmlElement("Quantity")]
    public int Quantity { get; set; }
    [XmlElement("Name")]
    public string Name { get; set; }
    public string Options { get; set; }

    public OrderedSandwich()
    {
        Quantity = 0;
        Name = null;
        Options = null;
    }

    public override string ToString()
    {
        string userEntry = Quantity + " " + Name;
        if (Options != null)
        {
            userEntry += " : " + Options;
        }
        return userEntry;
    }
}

public struct OrderedCommand
{
    [XmlElement("Sandwich")]
    public List<OrderedSandwich> Command { get; set; }
    
}

[XmlRoot("CommandList")]
public struct Commands
{
    [XmlElement("Command")]
    public List<OrderedCommand> CommandList { get; set; }
}
