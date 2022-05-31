namespace sandwichshop.Quantity
{
    public class Quantity
    {
        public double Value;
        public QuantityUnit QuantityUnit;

        public Quantity(double value, QuantityUnit quantityUnit)
        {
            Value = value;
            QuantityUnit = quantityUnit;
        }

        public override string ToString()
        {
            return $"{Value}{QuantityUnit}";
        }
    }
}