using Godot;
using System;

/*
 * Author: [Lam, Justin]
 * Last Updated: [03/19/2025]
 * [manages enemies]
 */

public partial class EnemyManager : Node
{
    public static EnemyManager _instance;


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

    /// <summary>
    /// returns singleton
    /// </summary>
    public static EnemyManager instance
    {
        get { return _instance; }
    }
}
