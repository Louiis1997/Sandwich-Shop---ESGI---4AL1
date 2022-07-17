using System;
using System.Collections.Generic;
using sandwichshop.Currencies;

namespace sandwichshop.Sandwiches;

public class SandwichFactory
{
    private Currencies.Currencies currencies;
    private List<Ingredient> Ingredients;

    public SandwichFactory(Currencies.Currencies currencies, List<Ingredient> Ingredients)
    {
        this.currencies = currencies;
        this.Ingredients = Ingredients;
    }

    public Sandwich CreateSandwich(string sandwich)
    {
        return sandwich.ToLower() switch
        {
            "dieppois" => new SandwichBuilder()
                .WithName("Dieppois")
                .WithPrice(new Price(4.50, currencies.Get(CurrencyName.Euro)))
                .WithIngredient(Ingredients.Find(ingredient => ingredient.Name == "pain"))
                .WithIngredient(Ingredients.Find(ingredient => ingredient.Name == "de thon"))
                .WithIngredient(Ingredients.Find(ingredient => ingredient.Name == "tomate"))
                .WithIngredient(Ingredients.Find(ingredient => ingredient.Name == "de mayonnaise"))
                .WithIngredient(Ingredients.Find(ingredient => ingredient.Name == "de salade"))
                .Build(),
            "jambon beurre" => new SandwichBuilder()
                .WithName("Jambon beurre")
                .WithPrice(new Price(3.50, currencies.Get(CurrencyName.Euro)))
                .WithIngredient(Ingredients.Find(ingredient => ingredient.Name == "pain"))
                .WithIngredient(Ingredients.Find(ingredient => ingredient.Name == "tranche de jambon"))
                .WithIngredient(Ingredients.Find(ingredient => ingredient.Name == "de beurre"))
                .Build(),
            "poulet crudités" => new SandwichBuilder()
                .WithName("Poulet crudités")
                .WithPrice(new Price(5, currencies.Get(CurrencyName.Euro)))
                .WithIngredient(Ingredients.Find(ingredient => ingredient.Name == "pain"))
                .WithIngredient(Ingredients.Find(ingredient => ingredient.Name =="oeuf"))
                .WithIngredient(Ingredients.Find(ingredient => ingredient.Name == "tomate"))
                .WithIngredient(Ingredients.Find(ingredient => ingredient.Name == "tranche de poulet"))
                .WithIngredient(Ingredients.Find(ingredient => ingredient.Name == "de mayonnaise"))
                .WithIngredient(Ingredients.Find(ingredient => ingredient.Name == "de salade"))
                .Build(),
            _ => throw new ArgumentException("Ce sandwich n'existe pas")
        };
    }
}