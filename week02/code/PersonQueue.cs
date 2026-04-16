public class PersonQueue
{
    private readonly List<Person> _queue = new();

    public int Length => _queue.Count;

    public void Enqueue(Person person)
    {
        // FIX: Changed from Insert(0, person) to Add(person) 
        // to ensure FIFO (First-In, First-Out) behavior.
        _queue.Add(person);
    }

    public Person Dequeue()
    {
        if (IsEmpty())
            throw new InvalidOperationException("The queue is empty.");

        var person = _queue[0];
        _queue.RemoveAt(0);
        return person;
    }

    public bool IsEmpty()
    {
        return Length == 0;
    }

    public override string ToString()
    {
        return $"[{string.Join(", ", _queue)}]";
    }
}