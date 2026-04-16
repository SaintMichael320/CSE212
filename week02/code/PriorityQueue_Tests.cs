using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class PriorityQueueTests
{
    // -----------------------------------------------------------------------
    // Requirement 1: Enqueue adds items to the back of the queue.
    // -----------------------------------------------------------------------

    [TestMethod]
    // Scenario: Enqueue three items and verify the queue's string representation
    // reflects insertion order (back-of-queue placement for each new item):
    //   Enqueue("apple", 1), Enqueue("banana", 3), Enqueue("cherry", 2)
    // Expected Result: "[apple (Pri:1), banana (Pri:3), cherry (Pri:2)]"
    // Defect(s) Found: No defect for Enqueue itself. Items are added to the back
    // of the internal list correctly. Test passes.
    public void TestPriorityQueue_EnqueueAddsToBack()
    {
        var pq = new PriorityQueue();
        pq.Enqueue("apple", 1);
        pq.Enqueue("banana", 3);
        pq.Enqueue("cherry", 2);

        Assert.AreEqual("[apple (Pri:1), banana (Pri:3), cherry (Pri:2)]", pq.ToString());
    }

    // -----------------------------------------------------------------------
    // Requirement 2: Dequeue removes and returns the highest-priority item.
    // -----------------------------------------------------------------------

    [TestMethod]
    // Scenario: Enqueue three items with distinct priorities:
    //   ("low", 1), ("high", 10), ("mid", 5)
    // Dequeue once.
    // Expected Result: "high" is returned (priority 10 is the highest).
    // Defect(s) Found:
    //   DEFECT 1 — Loop used `index < _queue.Count - 1`, skipping the last element.
    //              Here "high" is at index 1 (middle), so it happened to be found,
    //              but the missing removal (defect 2) caused the test to fail on
    //              a second dequeue.
    //   DEFECT 2 — _queue.RemoveAt(highPriorityIndex) was never called, so the
    //              dequeued item stayed in the list. The queue never shrank.
    // Fix: Changed loop bound to `_queue.Count` and added RemoveAt call.
    public void TestPriorityQueue_DequeueReturnsHighestPriority()
    {
        var pq = new PriorityQueue();
        pq.Enqueue("low", 1);
        pq.Enqueue("high", 10);
        pq.Enqueue("mid", 5);

        var result = pq.Dequeue();
        Assert.AreEqual("high", result);
    }

    // -----------------------------------------------------------------------
    // Requirement 2 (continued): Highest priority item is the LAST one added.
    // This specifically exercises the off-by-one loop defect.
    // -----------------------------------------------------------------------

    [TestMethod]
    // Scenario: Enqueue items where the highest-priority item is the last one added:
    //   ("a", 1), ("b", 2), ("c", 99)
    // Dequeue once.
    // Expected Result: "c" is returned (priority 99, located at the last index).
    // Defect(s) Found:
    //   DEFECT 1 — Loop condition `index < _queue.Count - 1` stopped before reaching
    //              the last index. "c" at index 2 was never evaluated, so "b" (index 1,
    //              priority 2) was incorrectly selected as the highest.
    //   DEFECT 2 — Item was never removed from the list (missing RemoveAt).
    // Fix: Loop now runs while `index < _queue.Count` so the last element is included.
    public void TestPriorityQueue_HighestPriorityIsLastItem()
    {
        var pq = new PriorityQueue();
        pq.Enqueue("a", 1);
        pq.Enqueue("b", 2);
        pq.Enqueue("c", 99);

        var result = pq.Dequeue();
        Assert.AreEqual("c", result);
    }

    // -----------------------------------------------------------------------
    // Requirement 3: Ties are broken by FIFO — the item closest to the front wins.
    // -----------------------------------------------------------------------

    [TestMethod]
    // Scenario: Enqueue two items with the same priority:
    //   ("first", 5), ("second", 5)
    // Dequeue once.
    // Expected Result: "first" is returned because it was enqueued earlier (FIFO).
    // Defect(s) Found:
    //   DEFECT 3 — The comparison used `>=` instead of `>`. When priorities were equal,
    //              highPriorityIndex was updated to the newer item, so "second" was
    //              returned instead of "first", violating FIFO.
    //   DEFECT 2 — Item was never removed (missing RemoveAt).
    // Fix: Changed comparison to strict `>` so ties keep the earlier index.
    public void TestPriorityQueue_TieBreakFIFO_TwoItems()
    {
        var pq = new PriorityQueue();
        pq.Enqueue("first", 5);
        pq.Enqueue("second", 5);

        var result = pq.Dequeue();
        Assert.AreEqual("first", result);
    }

    [TestMethod]
    // Scenario: Enqueue three items all with the same priority:
    //   ("first", 7), ("second", 7), ("third", 7)
    // Dequeue three times.
    // Expected Result: "first", "second", "third"  (pure FIFO when all priorities equal).
    // Defect(s) Found:
    //   DEFECT 3 — Same >= issue. "third" would be selected each time because it is
    //              always the last equal-priority item seen in the loop.
    //   DEFECT 2 — Items were never removed, so all three stayed in the list forever.
    // Fix: strict `>` comparison and RemoveAt.
    public void TestPriorityQueue_TieBreakFIFO_ThreeItems()
    {
        var pq = new PriorityQueue();
        pq.Enqueue("first", 7);
        pq.Enqueue("second", 7);
        pq.Enqueue("third", 7);

        Assert.AreEqual("first", pq.Dequeue());
        Assert.AreEqual("second", pq.Dequeue());
        Assert.AreEqual("third", pq.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue items with mixed priorities where two share the highest value:
    //   ("alpha", 5), ("beta", 10), ("gamma", 10), ("delta", 3)
    // Dequeue twice.
    // Expected Result: "beta" first (earliest with priority 10), then "gamma".
    // Defect(s) Found:
    //   DEFECT 3 — >= would select "gamma" (the second priority-10 item) first.
    //   DEFECT 2 — Items were never removed.
    // Fix: strict `>` and RemoveAt.
    public void TestPriorityQueue_TieBreakFIFO_MixedPriorities()
    {
        var pq = new PriorityQueue();
        pq.Enqueue("alpha", 5);
        pq.Enqueue("beta", 10);
        pq.Enqueue("gamma", 10);
        pq.Enqueue("delta", 3);

        Assert.AreEqual("beta", pq.Dequeue());
        Assert.AreEqual("gamma", pq.Dequeue());
    }

    // -----------------------------------------------------------------------
    // Requirement 4: Dequeue on an empty queue throws InvalidOperationException.
    // -----------------------------------------------------------------------

    [TestMethod]
    // Scenario: Call Dequeue on a queue that has never had any items added.
    // Expected Result: InvalidOperationException is thrown with message
    //                  "The queue is empty."
    // Defect(s) Found: No defect. The empty-queue guard was implemented correctly
    // in the original code. Test passes.
    public void TestPriorityQueue_EmptyQueueThrowsException()
    {
        var pq = new PriorityQueue();

        try
        {
            pq.Dequeue();
            Assert.Fail("Expected InvalidOperationException was not thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
        catch (AssertFailedException)
        {
            throw;
        }
        catch (Exception e)
        {
            Assert.Fail($"Unexpected exception of type {e.GetType()}: {e.Message}");
        }
    }

    [TestMethod]
    // Scenario: Enqueue one item, dequeue it, then dequeue again from the now-empty queue.
    // Expected Result: First dequeue returns the item; second dequeue throws
    //                  InvalidOperationException with "The queue is empty."
    // Defect(s) Found:
    //   DEFECT 2 — Because the item was never removed (missing RemoveAt), the queue
    //              appeared non-empty after the first dequeue, so no exception was
    //              thrown on the second call. The test failed at Assert.Fail.
    // Fix: RemoveAt ensures the queue actually empties after the last item is dequeued.
    public void TestPriorityQueue_EmptyAfterLastDequeue()
    {
        var pq = new PriorityQueue();
        pq.Enqueue("only", 1);

        pq.Dequeue(); // removes the only item

        try
        {
            pq.Dequeue();
            Assert.Fail("Expected InvalidOperationException was not thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
        catch (AssertFailedException)
        {
            throw;
        }
        catch (Exception e)
        {
            Assert.Fail($"Unexpected exception of type {e.GetType()}: {e.Message}");
        }
    }
}