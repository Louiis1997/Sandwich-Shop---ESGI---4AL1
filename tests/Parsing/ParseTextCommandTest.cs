using System;
using System.Collections.Generic;
using System.Linq;
using sandwichshop.Order;
using sandwichshop.Sandwiches;
using sandwichshop.Shop;
using Xunit;

namespace sandwichshop.tests.Parsing.CommandParsingInText;

public class ParseTextCommandTest
{
    private readonly SandwichShop _sandwichShop = SandwichShop.Initialize();

    [Fact]
    public void Correct_Text_Entry_Returns_Command_With_One_Sandwich()
    {
        const string userEntry = "1 dieppois";

        var command = new UserOrder();
        command.ParseCommand(_sandwichShop.Menu, _sandwichShop.ShopStock,
            _sandwichShop.SandwichFactory, _sandwichShop.IngredientFactory, _sandwichShop.Ingredients, userEntry);

        var expectedCommand = new Dictionary<Sandwich, int> { { _sandwichShop.Menu.FindSandwich("dieppois"), 1 } };

        Assert.Equal(expectedCommand.ToList(), command.GetSandwiches().ToList());
    }

    [Fact]
    public void Unknown_Sandwich_In_Text_Entry_Throws_Argument_Exception()
    {
        const string userEntry = "1 sandwich inconnu";

        var command = new UserOrder();

        Assert.Throws<ArgumentException>(() => command.ParseCommand(_sandwichShop.Menu, _sandwichShop.ShopStock,
            _sandwichShop.SandwichFactory, _sandwichShop.IngredientFactory, _sandwichShop.Ingredients, userEntry));
    }

    [Fact]
    public void Invalid_Text_Entry_Format_Throws_Format_Exception()
    {
        const string userEntry = "error user entry format";

        var command = new UserOrder();

        Assert.Throws<FormatException>(() => command.ParseCommand(_sandwichShop.Menu, _sandwichShop.ShopStock,
            _sandwichShop.SandwichFactory, _sandwichShop.IngredientFactory, _sandwichShop.Ingredients, userEntry));
    }
}