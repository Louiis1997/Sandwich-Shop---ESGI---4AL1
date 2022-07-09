using sandwichshop.Currencies;

namespace sandwichshop.Sandwiches;

public class Price
{
    public Currency Unit;
    public double Value;

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