using System;

namespace dotnetcore
{
    public class Price
    {
        public int Value;
        public PriceUnit Unit;

        public Price(int value, PriceUnit unit)
        {
            Value = value;
            Unit = unit;
        }
    }
}