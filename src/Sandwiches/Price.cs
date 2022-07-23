using System;
using System.Xml.Serialization;
using sandwichshop.Currencies;

namespace sandwichshop.Sandwiches;

[Serializable]
[XmlInclude(typeof(Currency))]
public class Price
{
    public Price(double value, Currency unit)
    {
        Value = value;
        Unit = unit;
    }

    // Default constructor required for XML serialization
    public Price()
    {
    }

    public Currency Unit { get; set; }
    public double Value { get; set; }

    public override string ToString()
    {
        return $"{Value}{Unit}";
    }

    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        if (obj.GetType() != GetType()) return false;
        var other = (Price)obj;
        return Value.Equals(other.Value) && Unit.Equals(other.Unit);
    }
}