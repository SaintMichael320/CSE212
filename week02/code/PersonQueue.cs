using System;
using System.Collections.Generic;

public class PersonQueue
{
    private readonly List<Person> _queue = new();

    public int Length => _queue.Count;

    /// <summary>
    /// Add a person to the back of the queue.
    /// </summary>
    public void Enqueue(Person person)
    {
        // FIX: Changed from _queue.Insert(0, person) to _queue.Add(person)
        // to ensure people are added to the back (FIFO).
        _queue.Add(person);
    }

    /// <summary>
    /// Remove and return the person at the front of the queue.
    /// </summary>
    public Person Dequeue()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("The queue is empty.");
        }

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