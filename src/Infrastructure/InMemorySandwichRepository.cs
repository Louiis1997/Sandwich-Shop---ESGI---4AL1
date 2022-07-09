using System;
using System.Collections.Generic;
using sandwichshop.Sandwiches;

namespace sandwichshop.infrastructure;

public class InMemorySandwichRepository
{
    private readonly List<Sandwich> _data = new();

    public void Add(Sandwich sandwich)
    {
        _data.Add(sandwich);
    }

    public void Delete(Sandwich sandwich)
    {
        _data.Remove(sandwich);
    }

    public Sandwich FindByName(string name)
    {
        Sandwich sandwich = null;
        for (var i = 0; i < _data.Count; i++)
            if (_data[i].Name == name)
                sandwich = _data[i];
        if (sandwich == null) Console.WriteLine("NoSuchSandwichName");
        return sandwich;
    }

    public List<Sandwich> FindAll()
    {
        return _data;
    }
}