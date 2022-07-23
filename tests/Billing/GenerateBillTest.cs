using System.Collections.Generic;
using System.Linq;
using sandwichshop.Billing;
using sandwichshop.Order;
using sandwichshop.Quantity;
using sandwichshop.Sandwiches;
using sandwichshop.Shop;
using Xunit;
using Xunit.Abstractions;

namespace sandwichshop.tests.Billing;

public class GenerateBillTest
{
    private readonly SandwichShop _sandwichShop = SandwichShop.Initialize();
    private readonly ITestOutputHelper _testOutputHelper;

    public GenerateBillTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void Generate_Bill_Should_Return_Bill()
    {
        const string userEntry = "1 dieppois";

        var bill = new Bill(_sandwichShop.QuantityUnits);
        bill.AddSandwich(_sandwichShop.Menu.FindSandwich("dieppois"),
            new Quantity.Quantity(1, new QuantityUnit(QuantityUnitName.None, "")));

        var generatedBill = bill.Generate(userEntry);

        Assert.NotNull(generatedBill);
        var expectedGeneratedBill =
            "\n========================================\n" +
            " ______         _\n" +
            "|  ____|       | |\n" +
            "| |__ __ _  ___| |_ _   _ _ __ ___\n" +
            "|  __/ _` |/ __| __| | | | '__/ _ \\\n" +
            "| | | (_| | (__| |_| |_| | | |  __/\n" +
            "|_|  \\__,_|\\___|\\__|\\__,_|_|  \\___|\n\n" +
            "1 dieppois";
        Assert.Equal(expectedGeneratedBill, generatedBill);
    }

    [Fact]
    public void Add_User_Command_In_Bill_Sets_Bill_Sandwiches()
    {
        const string userEntry = "1 dieppois";
        var userOrder = new UserOrder();
        userOrder.ParseCommand(_sandwichShop.Menu, _sandwichShop.ShopStock,
            _sandwichShop.SandwichFactory, _sandwichShop.IngredientFactory, _sandwichShop.Ingredients, userEntry);

        var bill = new Bill(_sandwichShop.QuantityUnits);
        bill.AddUserCommand(userOrder);

        var expectedSandwichesInBill = new Dictionary<Sandwich, Quantity.Quantity>
        {
            {
                _sandwichShop.Menu.FindSandwich("dieppois"),
                new Quantity.Quantity(1, new QuantityUnit(QuantityUnitName.None, ""))
            }
        };

        Assert.Single(bill.Sandwiches);
        Assert.Equal(expectedSandwichesInBill.Keys, bill.Sandwiches.Keys);
        Assert.Equal(expectedSandwichesInBill.Values.First().Value, bill.Sandwiches.Values.First().Value);
        Assert.Equal(expectedSandwichesInBill.Values.First().QuantityUnit, bill.Sandwiches.Values.First().QuantityUnit);
    }
}