using System.IO;
using sandwichshop.CLI;

namespace sandwichshop.Billing;

public class TextBill: IBillingMethod
{
    public void GetBill(Bill bill, string parsedCommandMessage)
    {
        var billName = BillUtils.GetBillName(ClientCli.TextMethod);
        File.WriteAllText(billName, bill.Generate(parsedCommandMessage));
        ClientCli.DisplayBillIsCreated(billName);
    }
}