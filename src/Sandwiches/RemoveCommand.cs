namespace sandwichshop.Sandwiches;

class RemoveCommand : SandwichCommand
{
    public RemoveCommand(Sandwich sandwich, Ingredient ingredient)
        : base(sandwich, ingredient)
    {
    }

    public override void Do()
    {
        _sandwich.Remove(_ingredient);
        _sandwich.Price = new Price(_sandwich.Price.Value + 0.5, _sandwich.Price.Unit);
    }

    public override void Undo()
    {
        _sandwich.Add(_ingredient);
        _sandwich.Price = new Price(_sandwich.Price.Value - 0.5, _sandwich.Price.Unit);
    }
}