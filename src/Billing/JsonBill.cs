using sandwichshop.CLI;
using sandwichshop.Command;

namespace sandwichshop.Billing;

public class JsonBill: IBillingMethod
{
    public void GetBill(Bill bill, string parsedCommandMessage)
    {
        bill.Generate(parsedCommandMessage);
        var billName = BillUtils.GetBillName(ClientCli.JsonMethod);
        JsonConverter<FinalBill>.Serialize(BillUtils.BillToFinalBill(bill), billName);
        ClientCli.DisplayBillIsCreated(billName);
    }
}