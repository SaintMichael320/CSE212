public class TakingTurnsQueue
{
    private readonly PersonQueue _people = new();

    public int Length => _people.Length;

    public void AddPerson(string name, int turns)
    {
        var person = new Person(name, turns);
        _people.Enqueue(person);
    }

    public Person GetNextPerson()
    {
        if (_people.IsEmpty())
        {
            throw new InvalidOperationException("No one in the queue.");
        }

        Person person = _people.Dequeue();

        if (person.Turns <= 0)
        {
            // Infinite turns: Always put them back at the end of the line.
            _people.Enqueue(person);
        }
        else if (person.Turns > 1)
        {
            // Finite turns: Subtract one and re-enqueue if turns remain.
            person.Turns -= 1;
            _people.Enqueue(person);
        }
        // If person.Turns was 1, they are not re-enqueued (they leave the queue).

        return person;
    }

    public override string ToString()
    {
        return _people.ToString();
    }
}