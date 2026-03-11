public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // PLAN:
        // Step 1: Create a new array of doubles with size 'length' to hold our results.
        // Step 2: Loop from 1 to 'length' (inclusive) using index i.
        // Step 3: At each iteration, calculate the multiple: number * i
        //         (e.g., for number=7: i=1 → 7, i=2 → 14, i=3 → 21, ...)
        // Step 4: Store each result in the array at position i-1 (since arrays are 0-indexed).
        // Step 5: After the loop, return the completed array.

        // Step 1: Create the result array sized to 'length'
        double[] result = new double[length];

        // Step 2-4: Loop through each multiple and store it
        for (int i = 1; i <= length; i++)
        {
            // Multiply number by the current position to get the ith multiple
            // Store at index i-1 because the array starts at 0
            result[i - 1] = number * i;
        }

        // Step 5: Return the filled array
        return result;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // PLAN:
        // Rotating RIGHT by 'amount' means the last 'amount' elements move to the front.
        // Example: {1,2,3,4,5,6,7,8,9} rotated right by 3 → {7,8,9,1,2,3,4,5,6}
        //   - The last 3 elements {7,8,9} become the new beginning.
        //   - The first 6 elements {1,2,3,4,5,6} follow after.
        //
        // Step 1: Find the split point — the index where the tail segment begins.
        //         splitIndex = data.Count - amount
        //         (e.g., 9 - 3 = 6, so tail starts at index 6)
        //
        // Step 2: Slice out the TAIL (last 'amount' elements) using GetRange.
        //         tail = data.GetRange(splitIndex, amount) → {7, 8, 9}
        //
        // Step 3: Slice out the HEAD (everything before the split) using GetRange.
        //         head = data.GetRange(0, splitIndex) → {1, 2, 3, 4, 5, 6}
        //
        // Step 4: Clear the original list so we can rebuild it in the new order.
        //
        // Step 5: Add the TAIL first (it becomes the new beginning).
        //
        // Step 6: Add the HEAD after (it follows the tail).
        //         Final result: {7, 8, 9, 1, 2, 3, 4, 5, 6}

        // Step 1: Calculate where the tail segment starts
        int splitIndex = data.Count - amount;

        // Step 2: Extract the tail — these elements will move to the front
        List<int> tail = data.GetRange(splitIndex, amount);

        // Step 3: Extract the head — these elements will follow the tail
        List<int> head = data.GetRange(0, splitIndex);

        // Step 4: Clear the original list to rebuild it
        data.Clear();

        // Step 5: Add the tail first (the rotated-to-front portion)
        data.AddRange(tail);

        // Step 6: Add the head after (the remaining elements)
        data.AddRange(head);
    }
}