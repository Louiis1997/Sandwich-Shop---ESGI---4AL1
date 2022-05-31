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
}