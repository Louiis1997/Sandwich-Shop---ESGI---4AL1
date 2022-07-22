using System.Xml.Serialization;

namespace sandwichshop.Currencies;

public class Currency
{
    public CurrencyName Name;
    public string Symbol { get; set; }
    
    public Currency(CurrencyName name, string symbol)
    {
        this.Name = name;
        this.Symbol = symbol;
    }

    public override string ToString()
    {
        return Symbol;
    }
}