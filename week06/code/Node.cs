public class Node
{
    public int Data { get; set; }
    public Node? Right { get; private set; }
    public Node? Left { get; private set; }

    public Node(int data)
    {
        this.Data = data;
    }

    /// <summary>
    /// Problem 1 - Insert Unique Values Only
    /// Added a guard at the top: if value == Data, return immediately
    /// so duplicate values are never inserted into the tree.
    /// </summary>
    public void Insert(int value)
    {
        if (value == Data)
            return; // Duplicate — do nothing

        if (value < Data)
        {
            // Insert to the left
            if (Left is null)
                Left = new Node(value);
            else
                Left.Insert(value);
        }
        else
        {
            // Insert to the right
            if (Right is null)
                Right = new Node(value);
            else
                Right.Insert(value);
        }
    }

    /// <summary>
    /// Problem 2 - Contains
    /// Mirrors the Insert logic: go left if smaller, right if larger.
    /// Returns true when the value is found, false when a null child is reached.
    /// </summary>
    public bool Contains(int value)
    {
        if (value == Data)
            return true;

        if (value < Data)
            return Left is not null && Left.Contains(value);
        else
            return Right is not null && Right.Contains(value);
    }

    /// <summary>
    /// Problem 4 - Tree Height
    /// Height = 1 (this node) + the taller of the two subtrees.
    /// A null child contributes 0, so a leaf node returns 1 + max(0, 0) = 1.
    /// </summary>
    public int GetHeight()
    {
        int leftHeight  = Left  is not null ? Left.GetHeight()  : 0;
        int rightHeight = Right is not null ? Right.GetHeight() : 0;

        return 1 + Math.Max(leftHeight, rightHeight);
    }
}