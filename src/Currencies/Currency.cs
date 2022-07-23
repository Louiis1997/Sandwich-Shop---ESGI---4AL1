using System;
using System.Xml.Serialization;

namespace sandwichshop.Currencies;

[Serializable]
[XmlInclude(typeof(Currency))]
public class Currency
{
    public CurrencyName Name;

    public Currency(CurrencyName name, string symbol)
    {
        Name = name;
        Symbol = symbol;
    }

    // Default constructor required for serialization
    public Currency()
    {
    }

    public string Symbol { get; set; }

    public override string ToString()
    {
        return Symbol;
    }
    
    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        if (obj.GetType() != GetType()) return false;
        var other = (Currency)obj;
        return Name.Equals(other.Name) && Symbol.Equals(other.Symbol);
    }
}