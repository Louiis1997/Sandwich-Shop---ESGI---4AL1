namespace sandwichshop.Quantity;

public class QuantityUnit
{
    public QuantityUnitName UnitName;
    public string Symbol;

    public QuantityUnit(QuantityUnitName unitName, string symbol)
    {
        this.UnitName = unitName;
        this.Symbol = symbol;
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

        return this.UnitName == other.UnitName;
    }
}