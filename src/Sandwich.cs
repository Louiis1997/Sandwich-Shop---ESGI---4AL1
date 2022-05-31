using System;
using System.Collections.Generic;

namespace dotnetcore
{
    public class Sandwich
    { 
        public String Name;
        public List<Ingredient> Ingredients; 
        public Price Price;

        public Sandwich(String name, List<Ingredient> ingredients, Price price)
        {
            Name = name;
            Ingredients = ingredients;
            Price = price;
        }
    }
}