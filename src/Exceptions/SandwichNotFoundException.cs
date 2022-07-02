using System;

namespace sandwichshop.Exceptions;

public class SandwichNotFoundException : Exception
{
    public string ClientMessageForCli { get; set; }

    public SandwichNotFoundException(string sandwichName)
        : base(sandwichName)
    {
        ClientMessageForCli = $"Le sandwich '{sandwichName}' n'est pas dans le menu";
    }

    public SandwichNotFoundException(string sandwichName, Exception inner)
        : base(sandwichName, inner)
    {
        ClientMessageForCli = $"Le sandwich '{sandwichName}' n'est pas dans le menu";
    }
}