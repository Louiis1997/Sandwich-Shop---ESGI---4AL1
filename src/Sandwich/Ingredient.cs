using System;

namespace sandwichshop.Sandwich
{
    public class Ingredient
    {
        public readonly Quantity.Quantity Quantity;
        public readonly string Name;

        public Ingredient(Quantity.Quantity quantity, string name)
        {
            Quantity = quantity;
            Name = name;
        }
        
        public override string ToString()
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
        
        public override int GetHashCode()
        {
            return Quantity.GetHashCode() + Name.GetHashCode();
        }
    }
}