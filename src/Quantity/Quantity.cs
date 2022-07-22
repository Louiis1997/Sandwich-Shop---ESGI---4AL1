using System.Xml.Serialization;

namespace sandwichshop.Quantity;
public class Quantity
{
    public double Value { get; set; }
    public QuantityUnit QuantityUnit { get; set; }

    public Quantity(double value, QuantityUnit quantityUnit)
    {
        Value = value;
        QuantityUnit = quantityUnit;
    }

    public override string ToString()
    {
        return $"{Value}{QuantityUnit}";
    }
}
