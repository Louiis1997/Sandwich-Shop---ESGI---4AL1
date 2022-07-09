namespace sandwichshop.infrastructure;

using System;
using System.Collections.Generic;
using Sandwich;

public class InMemorySandwichRepository
{
    private readonly List<Sandwich> _data = new ();
    public void Add(Sandwich sandwich)
    {
        _data.Add(sandwich);
    }

    public void Delete(Sandwich sandwich)
    {
        _data.Remove(sandwich);
    }

    public Sandwich FindByName(String name)
    {
        Sandwich sandwich = null;
        for (int i = 0; i < _data.Count; i++)
        {
            if (_data[i].Name == name)
            {
                sandwich = _data[i];
            }
        }
        if (sandwich == null)
        {
            Console.WriteLine("NoSuchSandwichName");
        }
        return sandwich;
    }

    public List<Sandwich> FindAll()
    {
        return _data;
    }
}
