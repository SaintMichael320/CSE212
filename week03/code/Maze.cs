using System;
using System.Collections.Generic;

/// <summary>
/// Defines a maze using a dictionary. The dictionary is provided by the
/// user when the Maze object is created. The dictionary will contain the
/// following mapping:
///
/// (x,y) : [left, right, up, down]
///
/// If a direction is false, there is a wall.
/// If true, movement is allowed.
/// </summary>
public class Maze
{
    private readonly Dictionary<(int, int), bool[]> _mazeMap;
    private int _currX = 1;
    private int _currY = 1;

    public Maze(Dictionary<(int, int), bool[]> mazeMap)
    {
        _mazeMap = mazeMap;
    }

    /// <summary>
    /// Move left if possible, otherwise throw exception
    /// </summary>
    public void MoveLeft()
    {
        var directions = _mazeMap[(_currX, _currY)];

        if (!directions[0])
            throw new InvalidOperationException("Can't go that way!");

        _currX--;
    }

    /// <summary>
    /// Move right if possible, otherwise throw exception
    /// </summary>
    public void MoveRight()
    {
        var directions = _mazeMap[(_currX, _currY)];

        if (!directions[1])
            throw new InvalidOperationException("Can't go that way!");

        _currX++;
    }

    /// <summary>
    /// Move up if possible, otherwise throw exception
    /// </summary>
    public void MoveUp()
    {
        var directions = _mazeMap[(_currX, _currY)];

        if (!directions[2])
            throw new InvalidOperationException("Can't go that way!");

        _currY--;
    }

    /// <summary>
    /// Move down if possible, otherwise throw exception
    /// </summary>
    public void MoveDown()
    {
        var directions = _mazeMap[(_currX, _currY)];

        if (!directions[3])
            throw new InvalidOperationException("Can't go that way!");

        _currY++;
    }

    public string GetStatus()
    {
        return $"Current location (x={_currX}, y={_currY})";
    }
}