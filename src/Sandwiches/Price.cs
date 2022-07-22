using sandwichshop.Currencies;

namespace sandwichshop.Sandwiches;

public class Price
{
    public Currency Unit { get; set; }
    public double Value { get; set; }

    public Price(double value, Currency unit)
    {
        Value = value;
        Unit = unit;
    }

    public override string ToString()
    {
        return $"{Value}{Unit}";
    }
}