using System;

namespace dotnetcore
{
    public class Price
    {
        public double Value;
        public PriceUnit Unit;

        public Price(double value, PriceUnit unit)
        {
            Value = value;
            Unit = unit;
        }
    }
}