using System.Collections.Generic;

namespace sandwichshop.Quantity;

public class QuantityUnits
{
    public List<QuantityUnit> Values;
    
    public QuantityUnits()
    {
        Values = new List<QuantityUnit>();
    }

    public void Add(QuantityUnitName unitName, string symbol)
    {
        Values.Add(new QuantityUnit(unitName, symbol));
    }
    
    public QuantityUnit Get(QuantityUnitName unitName)
    {
        foreach (QuantityUnit c in Values)
        {
            if (c.UnitName == unitName)
            {
                return c;
            }
        }
        return null;
    }
}