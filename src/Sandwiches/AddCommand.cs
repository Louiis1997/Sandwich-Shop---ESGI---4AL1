using sandwichshop.Currencies;

namespace sandwichshop.Sandwiches;

class AddCommand : SandwichCommand
{
    public AddCommand(Sandwich sandwich, Ingredient ingredient)
        : base(sandwich, ingredient)
    {
    }

    public override void Do()
    {
        _sandwich.Add(_ingredient);
        _sandwich.Price = new Price(_sandwich.Price.Value + 1, _sandwich.Price.Unit);
    }

    public override void Undo()
    {
        _sandwich.Remove(_ingredient);
        _sandwich.Price = new Price(_sandwich.Price.Value - 1, _sandwich.Price.Unit);
    }
}