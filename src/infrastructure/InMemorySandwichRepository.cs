using System;
using System.Collections.Generic;

namespace dotnetcore.infrastructure
{
    public class InMemorySandwichRepository
    {
        public List<Sandwich> data = new List<Sandwich>();
        public void Add(Sandwich sandwich)
        {
            data.Add(sandwich);
        }

        public void Delete(Sandwich sandwich)
        {
            data.Remove(sandwich);
        }

        public Sandwich FindByName(String name)
        {
            Sandwich sandwich = null;
            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].Name == name)
                {
                    sandwich = data[i];
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
            return data;
        }
    }
}