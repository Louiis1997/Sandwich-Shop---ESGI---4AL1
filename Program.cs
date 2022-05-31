using System;
using System.Collections.Generic;

namespace dotnetcore
{
    class Program
    {
        static void Main(string[] args)
        {
            Ingredient pain = new Ingredient(new Quantity(1,QuantityUnit.None), "pain");
            Ingredient trancheDeJambon = new Ingredient(new Quantity(1,QuantityUnit.None), "tranche de jambon");
            Ingredient beurre = new Ingredient(new Quantity(10,QuantityUnit.Gram), "de beurre");
            List<Ingredient> jambonBeurre = new List<Ingredient>();
            jambonBeurre.Add(pain);
            jambonBeurre.Add(trancheDeJambon);
            jambonBeurre.Add(beurre);
            Sandwich sandwich = new Sandwich("Jambon beurre", jambonBeurre , new Price(3.50,PriceUnit.Euro));

            Console.WriteLine(sandwich.Name + sandwich.Price.Value + sandwich.Price.Unit);
        }
    }
}
