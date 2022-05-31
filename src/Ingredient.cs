using System;

namespace dotnetcore
{
    public class Ingredient
    {
        public Quantity Quantity;
        public String Name;

        public Ingredient(Quantity quantity, String name)
        {
            Quantity = quantity;
            Name = name;
        }
    }
}