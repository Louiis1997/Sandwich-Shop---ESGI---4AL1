namespace sandwichshop.Currencies;

using System.Collections.Generic;

public class Currencies
{
    public List<Currency> Values;

    public Currencies()
    {
        Values = new List<Currency>();
    }

    public void Add(CurrencyName currency, string symbol)
    {
        Values.Add(new Currency(currency, symbol));
    }
    
    public Currency Get(CurrencyName currency)
    {
        foreach (Currency c in Values)
        {
            if (c.Name == currency)
            {
                return c;
            }
        }
        return null;
    }
}