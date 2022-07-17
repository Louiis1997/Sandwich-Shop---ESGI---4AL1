namespace sandwichshop.Sandwiches;

abstract class SandwichCommand
{
    protected readonly Sandwich _sandwich;
    protected readonly Ingredient _ingredient;
    
    public SandwichCommand(Sandwich sandwich, Ingredient ingredient)
    {
        _sandwich = sandwich;
        _ingredient = ingredient;
    }
    
    public abstract void Do();
    public abstract void Undo();
}
