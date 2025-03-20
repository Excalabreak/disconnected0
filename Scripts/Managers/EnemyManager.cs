using Godot;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;

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
    private Vector2 _resolution;

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

        _resolution = GetViewport().GetVisibleRect().Size;
    }

    public void StartLevel(Planets planet)
    {
        _currentEnemies = new List<Node2D>();

        for (int i = 0; i < _levels[(int)planet]._enemies.Length; i++)
        {
            if (_levels[(int)planet]._enemies[i] > 0)
            {
                for (int j = 0; j < _levels[(int)planet]._enemies[i]; j++)
                {
                    Node2D enemy = _enemyScenes[i].Instantiate<Node2D>() as Node2D;
                    enemy.GlobalPosition = new Vector2(-50, (float)GD.RandRange(0, _resolution.Y));
                    _currentEnemies.Add(enemy);
                    GetTree().Root.AddChild(enemy);
                }
            }
        }
    }

    public bool CheckLevelComplete()
    {
        foreach (Node2D enemy in _currentEnemies)
        {
            if (IsInstanceValid(enemy))
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// returns singleton
    /// </summary>
    public static EnemyManager instance
    {
        get { return _instance; }
    }
}
