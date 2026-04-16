using System;

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

        // Pull the next person from the front
        Person person = _people.Dequeue();

        if (person.Turns <= 0)
        {
            // FIX: If turns are 0 or negative, they stay in the queue forever.
            _people.Enqueue(person);
        }
        else if (person.Turns > 1)
        {
            // FIX: If they have more than 1 turn left, decrement and re-enqueue.
            person.Turns -= 1;
            _people.Enqueue(person);
        }
        // If person.Turns is exactly 1, they take their final turn and are NOT re-enqueued.

        return person;
    }

    public override string ToString()
    {
        return _people.ToString();
    }
}