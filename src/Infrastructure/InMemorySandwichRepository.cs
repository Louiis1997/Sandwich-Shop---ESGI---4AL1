using System;
using System.Collections.Generic;

namespace sandwichshop.infrastructure
{
    public class InMemorySandwichRepository
    {
        public List<sandwichshop.Sandwich.Sandwich> data = new List<sandwichshop.Sandwich.Sandwich>();
        public void Add(sandwichshop.Sandwich.Sandwich sandwich)
        {
            data.Add(sandwich);
        }

        public void Delete(sandwichshop.Sandwich.Sandwich sandwich)
        {
            data.Remove(sandwich);
        }

        public sandwichshop.Sandwich.Sandwich FindByName(String name)
        {
            sandwichshop.Sandwich.Sandwich sandwich = null;
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

        public List<sandwichshop.Sandwich.Sandwich> FindAll()
        {
            return data;
        }
    }
}