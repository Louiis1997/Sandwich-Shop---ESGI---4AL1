using sandwichshop.Currencies;

namespace sandwichshop.Sandwich
{
    public class Price
    {
        public double Value;
        public Currency Unit;

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
}