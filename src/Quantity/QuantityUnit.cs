namespace sandwichshop.Quantity;

public class QuantityUnit
{
    public readonly QuantityUnitName UnitName;
    private readonly string _symbol;

    public QuantityUnit(QuantityUnitName unitName, string symbol)
    {
        UnitName = unitName;
        _symbol = symbol;
    }

    public override string ToString()
    {
        return _symbol;
    }
    
    public override bool Equals(object obj)
    {
        if (obj == null)
            return false;

        QuantityUnit other = obj as QuantityUnit;
        if (other == null)
            return false;

        return UnitName == other.UnitName;
    }
    
    public override int GetHashCode()
    {
        return UnitName.GetHashCode();
    }
}