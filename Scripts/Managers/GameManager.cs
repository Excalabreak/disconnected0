using Godot;
using System;

/*
 * Author: [Lam, Justin]
 * Last Updated: [03/04/2025]
 * [Game manager for game]
 */

public partial class GameManager : Node
{
    private static GameManager _instance;

    [Export] private Node2D _currentPlayer;
    private Planets _currentLevel = Planets.PLUTO;

    /// <summary>
    /// sets up singleton
    /// </summary>
    public override void _Ready()
    {
        if (_instance != null)
        {
            QueueFree();
        }

        _instance = this;
    }

    public Node2D currentPlayer
    {
        get { return _currentPlayer; }
    }

    public static GameManager instance
    {
        get { return _instance; }
    }
}
