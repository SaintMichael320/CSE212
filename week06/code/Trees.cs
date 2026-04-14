public static class Trees
{
    /// <summary>
    /// Given a sorted list (sorted_list), create a balanced BST.  If the values in the
    /// sortedNumbers were inserted in order from left to right into the BST, then it
    /// would resemble a linked list (unbalanced). To get a balanced BST, the
    /// InsertMiddle function is called to find the middle item in the list to add
    /// first to the BST. The InsertMiddle function takes the whole list but also takes
    /// a range (first to last) to consider.  For the first call, the full range of 0 to
    /// Length-1 used.
    /// </summary>
    public static BinarySearchTree CreateTreeFromSortedList(int[] sortedNumbers)
    {
        var bst = new BinarySearchTree(); // Create an empty BST to start with 
        InsertMiddle(sortedNumbers, 0, sortedNumbers.Length - 1, bst);
        return bst;
    }

    /// <summary>
    /// Problem 5 - Create Tree from Sorted List
    ///
    /// Base case: if first > last, the sub-range is empty — do nothing.
    ///
    /// Otherwise:
    ///   1. Find the middle index of the current range.
    ///   2. Insert the value at that middle index into the BST.
    ///   3. Recursively do the same for the left half  (first  .. mid - 1).
    ///   4. Recursively do the same for the right half (mid + 1 .. last).
    ///
    /// For sortedNumbers = {10, 20, 30, 40, 50, 60}, first = 0, last = 5:
    ///   Insertion order: 30, 10, 20, 50, 40, 60  →  balanced BST.
    ///
    /// No list slicing is used — only index arithmetic on the original array.
    /// </summary>
    /// <param name="sortedNumbers">input numbers that are already sorted</param>
    /// <param name="first">the first index in the sortedNumbers to insert</param>
    /// <param name="last">the last index in the sortedNumbers to insert</param>
    /// <param name="bst">the BinarySearchTree in which to insert the values</param>
    private static void InsertMiddle(int[] sortedNumbers, int first, int last, BinarySearchTree bst)
    {
        if (first > last)
            return; // Base case: empty range

        int mid = (first + last) / 2;
        bst.Insert(sortedNumbers[mid]);          // Insert middle value

        InsertMiddle(sortedNumbers, first, mid - 1, bst);  // Left half
        InsertMiddle(sortedNumbers, mid + 1, last, bst);   // Right half
    }
}