using System;

namespace sandwichshop.Quantity;

[Serializable]
public class Quantity
{
    public Quantity(double value, QuantityUnit quantityUnit)
    {
        Value = value;
        QuantityUnit = quantityUnit;
    }

    public Quantity()
    {
    }

    public double Value { get; set; }
    public QuantityUnit QuantityUnit { get; set; }

    public override string ToString()
    {
        return $"{Value}{QuantityUnit}";
    }
}