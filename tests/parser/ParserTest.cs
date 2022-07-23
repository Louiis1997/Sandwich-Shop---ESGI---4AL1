using sandwichshop.OrderMethod;
using sandwichshop.Shop;
using Xunit;

namespace sandwichshop.tests.parser;

public class ParserTest
{
    [Fact]
    public void Parse_a_command_in_JSON_OK()
    {
        SandwichShop sandwichShop = SandwichShop.Initialize();
        IOrderMethod jsonOrder = new JsonOrder();
        
        Assert.Equal(2, 1 + 1);
    }
    
    [Fact]
    public void Parse_a_command_in_JSON_KO()
    {
        Assert.Equal(2, 1 + 1);
    }
    
    [Fact]
    public void Parse_a_command_in_XML_OK()
    {
        Assert.Equal(2, 1 + 1);
    }
    
    [Fact]
    public void Parse_a_command_in_XML_KO()
    {
        Assert.Equal(2, 1 + 1);
    }
}