namespace sandwichshop.Quantity;

public class QuantityUnit
{
    public readonly QuantityUnitName UnitName;
    public  string Symbol { get; set; }

    public QuantityUnit(QuantityUnitName unitName, string symbol)
    {
        UnitName = unitName;
        Symbol = symbol;
    }

    public override string ToString()
    {
        return Symbol;
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