using System;

namespace dotnetcore
{
    public class Quantity
    {
        public double Value;
        public QuantityUnit Unit;

        public Quantity(double value, QuantityUnit unit)
        {
            Value = value;
            Unit = unit;
        }
    }
}