using System;
using sandwichshop.CLI;
using sandwichshop.Shop;

try
{
    var shop = SandwichShop.Initialize();
    shop.OpenForCommand();
}
catch (Exception e)
{
    ClientCli.DisplayException(e);
}

// TODO : Revoir les responsabilités des classes
// TODO : Voir s'il faut plus de services (Command readline, etc.)