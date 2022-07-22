using sandwichshop.CLI;

namespace sandwichshop.Billing;

public class CliBill: IBillingMethod
{
    public void GetBill(Bill bill, string parsedCommandMessage)
    {
        ClientCli.DisplayBill(bill, parsedCommandMessage);
        ClientCli.DisplayDoubleLineSeparation();
    }
}