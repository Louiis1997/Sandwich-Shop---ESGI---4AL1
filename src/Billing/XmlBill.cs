using sandwichshop.CLI;
using sandwichshop.Command;

namespace sandwichshop.Billing;

public class XmlBill : IBillingMethod
{
    public void GetBill(Bill bill, string parsedCommandMessage)
    {
        bill.Generate(parsedCommandMessage);
        var billName = BillUtils.GetBillName(ClientCli.XmlMethod);
        var converter = new XmlConverter<FinalBill>();
        converter.Serialize(BillUtils.BillToFinalBill(bill), billName);
        ClientCli.DisplayBillIsCreated(billName);
    }
}