using System;
using sandwichshop.Billing;
using sandwichshop.CLI;
using sandwichshop.Shop;

namespace sandwichshop.OrderMethod;

public abstract class IOrderMethod
{
    public abstract void Order(SandwichShop sandwichShop);

    public void SelectBillingMethod(Bill bill, string parsedCommandMessage)
    {
        IBillingMethod billingMethod = null;
        try
        {
            switch (ClientCli.SelectBillingMethod())
            {
                case ClientCli.CliMethod:
                    billingMethod = new CliBill();
                    break;
                case ClientCli.TextMethod:
                    billingMethod = new TextBill();
                    break;
                case ClientCli.JsonMethod:
                    billingMethod = new JsonBill();
                    break;
                case ClientCli.XmlMethod:
                    billingMethod = new XmlBill();
                    break;
                case ClientCli.QuitString:
                    break;
                default:
                    ClientCli.DisplayUnexpectedBillingMethod();
                    break;
            }

            billingMethod?.GetBill(bill, parsedCommandMessage);
        }
        catch (Exception e)
        {
            ClientCli.DisplayString("Une erreur s'est produite lors de la sélection de la méthode de facturation");
        }
    }
}