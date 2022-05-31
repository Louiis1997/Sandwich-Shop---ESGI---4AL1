using System;

namespace dotnetcore
{
    public class Quantity
    {
        public int Value;
        public QuantityUnit Unit;

        public Quantity(int value, QuantityUnit unit)
        {
            Value = value;
            Unit = unit;
        }
    }
}