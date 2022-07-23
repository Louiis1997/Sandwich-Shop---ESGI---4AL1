using System;
using System.Xml.Serialization;

namespace sandwichshop.Quantity;

[Serializable]
[XmlInclude(typeof(QuantityUnitName))]
public class QuantityUnit
{
    public readonly QuantityUnitName UnitName;

    public QuantityUnit(QuantityUnitName unitName, string symbol)
    {
        UnitName = unitName;
        Symbol = symbol;
    }

    // Default constructor required for XML serialization
    public QuantityUnit()
    {
        UnitName = QuantityUnitName.None;
    }

    public string Symbol { get; set; }

    public override string ToString()
    {
        return Symbol;
    }

    public override bool Equals(object obj)
    {
        if (obj is not QuantityUnit other)
            return false;

        return UnitName == other.UnitName;
    }

    public override int GetHashCode()
    {
        return UnitName.GetHashCode();
    }
}