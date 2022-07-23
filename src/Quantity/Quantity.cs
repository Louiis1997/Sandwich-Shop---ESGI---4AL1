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

    public override bool Equals(object obj)
    {
        if (obj == null)
            return false;

        if (obj.GetType() != typeof(Quantity))
            return false;

        var other = (Quantity)obj;
        return Math.Abs(Value - other.Value) < 0.1 && QuantityUnit == other.QuantityUnit;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode() ^ QuantityUnit.GetHashCode();
    }
}