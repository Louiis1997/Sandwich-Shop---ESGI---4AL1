using System.Collections.Generic;
using System.Xml.Serialization;
using sandwichshop.Sandwiches;

namespace sandwichshop.Billing;

public class BillUtils
{
    private static int _billNumber = 1;
    public static FinalBill BillToFinalBill(Bill bill)
    {
        List<CommandBill> result = new List<CommandBill>();
        foreach (var (sandwich, quantity) in bill.Sandwiches)
        {
            result.Add(new CommandBill(sandwich, quantity));
        }

        return new FinalBill(result, bill.TotalPrice);
    }

    public static string GetBillName(string format)
    {
        var billName = "../../../billsFolder/Facture"+_billNumber+"."+format;
        _billNumber++;
        return billName;
    }
}


[XmlRoot("Bill")]
public struct FinalBill
{
    [XmlArray("Commands")]
    [XmlArrayItem("Command")]
    public List<CommandBill> CommandBills { get; }
    public double TotalPrice { get; set; }

    public FinalBill(List<CommandBill> commandBills, double totalPrice)
    {
        CommandBills = commandBills;
        TotalPrice = totalPrice;
    }
}

public struct CommandBill
{
    [XmlIgnore] public Sandwich Sandwich { get; set; }
    [XmlIgnore] public Quantity.Quantity Quantity { get; set; }

    public CommandBill(Sandwich sandwich, Quantity.Quantity quantity)
    {
        Sandwich = sandwich;
        Quantity = quantity;
    }
}
