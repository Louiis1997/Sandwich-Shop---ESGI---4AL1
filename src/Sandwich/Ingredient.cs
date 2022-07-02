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
        
        public override String ToString()
        {
            return Quantity + " " + Name;
        }
        
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            Ingredient other = (Ingredient)obj;
            return this.Quantity.Equals(other.Quantity) && this.Name.Equals(other.Name);
        }
    }
}