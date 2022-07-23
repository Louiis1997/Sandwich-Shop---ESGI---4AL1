using System;
using System.Xml.Serialization;

namespace sandwichshop.Sandwiches;

[Serializable]
[XmlInclude(typeof(Quantity.Quantity))]
public class Ingredient
{
    public Ingredient(Quantity.Quantity quantity, string name)
    {
        Quantity = quantity;
        Name = name;
    }

    // Default constructor required for XML serialization
    public Ingredient()
    {
    }

    public string Name { get; set; }
    public Quantity.Quantity Quantity { get; set; }

    public override string ToString()
    {
        return Quantity + " " + Name;
    }

    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        if (obj.GetType() != GetType()) return false;
        var other = (Ingredient)obj;
        return Name.Equals(other.Name);
    }

    public override int GetHashCode()
    {
        return Quantity.GetHashCode() + Name.GetHashCode();
    }
}