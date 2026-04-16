public class Person
{
    public readonly string Name;
    public int Turns { get; set; }

    internal Person(string name, int turns)
    {
        Name = name;
        Turns = turns;
    }

    public override string ToString()
    {
        // Helpful for debugging: shows "Forever" for 0 or negative turns.
        return Turns <= 0 ? $"({Name}:Forever)" : $"({Name}:{Turns})";
    }
}