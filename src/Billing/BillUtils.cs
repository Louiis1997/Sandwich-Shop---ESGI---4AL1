using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using sandwichshop.Sandwiches;

namespace sandwichshop.Billing;

public class BillUtils
{
    private static int _billNumber = 1;

    public static FinalBill BillToFinalBill(Bill bill)
    {
        var result = new List<CommandBill>();
        foreach (var (sandwich, quantity) in bill.Sandwiches) result.Add(new CommandBill(sandwich, quantity));

        return new FinalBill(result, bill.TotalPrice);
    }

    public static string GetBillName(string format)
    {
        // Generate file name according to bill generated date and time
        var fileName = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        var billName = "../../../billsFolder/" + fileName + "-facture" + "." + format;
        _billNumber++;
        return billName;
    }
}

[Serializable]
[XmlInclude(typeof(CommandBill))]
[XmlRoot]
public struct FinalBill
{
    public List<CommandBill> CommandBills { get; }
    public double TotalPrice { get; set; }

    public FinalBill(List<CommandBill> commandBills, double totalPrice)
    {
        CommandBills = commandBills;
        TotalPrice = totalPrice;
    }
}

[Serializable]
[XmlInclude(typeof(Quantity.Quantity))]
public struct CommandBill
{
    public Sandwich Sandwich { get; set; }
    public Quantity.Quantity Quantity { get; set; }

    public CommandBill(Sandwich sandwich, Quantity.Quantity quantity)
    {
        Sandwich = sandwich;
        Quantity = quantity;
    }
}