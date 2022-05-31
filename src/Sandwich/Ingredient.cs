using System;

namespace sandwichshop.Sandwich
{
    public class Ingredient
    {
        public Quantity.Quantity Quantity;
        public String Name;

        public Ingredient(Quantity.Quantity quantity, String name)
        {
            Quantity = quantity;
            Name = name;
        }
    }
}