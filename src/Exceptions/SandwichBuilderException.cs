using System;

namespace sandwichshop.Exceptions;

public class SandwichBuilderException : Exception
{
    public string ClientMessageForCli { get; set; }

    public SandwichBuilderException(string message)
        : base(message)
    {
        ClientMessageForCli = message;
    }

    public SandwichBuilderException(string message, Exception inner)
        : base(message, inner)
    {
        ClientMessageForCli = message;
    }
}