namespace sandwichshop.Billing;

public interface IBillingMethod
{
    public void GetBill(Bill bill, string parsedCommandMessage);
}