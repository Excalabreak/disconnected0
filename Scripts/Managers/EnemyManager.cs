using Godot;
using System;
using System.Collections.Generic;

/*
 * Author: [Lam, Justin]
 * Last Updated: [03/19/2025]
 * [manages enemies]
 */

public partial class EnemyManager : Node
{
    public static EnemyManager _instance;

    [Export] private LevelResources[] _levels;
    [Export] private PackedScene[] _enemyScenes;

    private List<Node2D> _currentEnemies;

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

        _currentEnemies = new List<Node2D>();
    }

    public void StartLevel(Planets planet)
    {
        for (int i = 0; i < _levels[(int)planet]._enemies.Length; i++)
        {
            if (_levels[(int)planet]._enemies[i] > 0)
            {
                for (int j = 0; j < _levels[(int)planet]._enemies[j]; j++)
                {

                }
            }
        }
    }

    /// <summary>
    /// returns singleton
    /// </summary>
    public static EnemyManager instance
    {
        get { return _instance; }
    }
}
