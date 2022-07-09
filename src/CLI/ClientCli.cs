using System;
using sandwichshop.Billing;
using sandwichshop.Shop;

namespace sandwichshop.CLI;

public static class ClientCli
{
    public const string QuitString = "q";

    public static void DisplayMenu(Menu menu)
    {
        menu.DisplayMenu();
    }

    public static string RetrieveClientEntry()
    {
        var userEntry = "";
        while (userEntry == "")
        {
            Console.Write(
                $"\nEntrez votre commande (exemple: '1 dieppois, 4 jambon beurre') (tapez '{QuitString}' puis 'entrée' pour quitter) : ");
            userEntry = Console.ReadLine();
            if (userEntry != "") return userEntry;
        }

        throw new Exception("Shouldn't have passed here");
    }

    public static void DisplayBill(Bill bill, string parsedCommandMessage)
    {
        Console.WriteLine(bill.Generate(parsedCommandMessage));
    }

    public static void DisplayUnexpectedCommandFormatError(Exception e)
    {
        DisplayDoubleLineSeparation();
        Console.WriteLine("Votre commande ne correspond pas au format attendu :");
        DisplayException(e);
        DisplayDoubleLineSeparation(true);
    }

    public static bool AskUserWantsToReorder()
    {
        Console.WriteLine("Voulez-vous faire une autre commande ? O/n");
        var endProgramOrContinue = Console.ReadLine();
        if (endProgramOrContinue != null && endProgramOrContinue.ToLower() == "n")
        {
            DisplaySeeYouNextTime();
            return false;
        }

        return true;
    }

    public static void DisplaySeeYouNextTime()
    {
        DisplayDoubleLineSeparation();
        Console.WriteLine("À la prochaine !");
        DisplayDoubleLineSeparation();
    }

    public static void DisplayException(Exception e)
    {
        Console.WriteLine(e.Message);
    }

    public static void DisplayDoubleLineSeparation(bool withNewLineAtEnd = false)
    {
        Console.WriteLine("====================================================" + (withNewLineAtEnd ? "\n" : ""));
    }
}