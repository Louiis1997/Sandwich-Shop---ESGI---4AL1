using System.Collections.Generic;
using System.Xml.Serialization;

namespace sandwichshop.Sandwiches;

public class OrderedSandwich
{
    [XmlElement("Quantity")]
    public int Quantity { get; set; }
    [XmlElement("Name")]
    public string Name { get; set; }

    public OrderedSandwich()
    {
        Quantity = 0;
        Name = null;
    }

    public override string ToString()
    {
        return Quantity + " " + Name;
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