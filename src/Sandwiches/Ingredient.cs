namespace sandwichshop.Sandwiches;

public class Ingredient
{
    public readonly string Name;
    public readonly Quantity.Quantity Quantity;

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
        if (obj == null) return false;
        if (obj.GetType() != GetType()) return false;
        var other = (Ingredient)obj;
        return Name.Equals(other.Name);
    }

    public override int GetHashCode()
    {
        return Quantity.GetHashCode() + Name.GetHashCode();
    }
}