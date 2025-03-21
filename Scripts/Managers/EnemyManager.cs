using Godot;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;

/*
 * Author: [Lam, Justin]
 * Last Updated: [03/21/2025]
 * [manages enemies]
 */

public partial class EnemyManager : Node
{
    public static EnemyManager _instance;

    [Export] private LevelResources[] _levels;
    [Export] private PackedScene[] _enemyScenes;

    private List<Node2D> _currentEnemies;
    private List<Node2D> _currentBombs;
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

        _currentBombs = new List<Node2D>();

        _resolution = GetViewport().GetVisibleRect().Size;
    }

    /// <summary>
    /// spawns the enemy based on the resource
    /// </summary>
    /// <param name="planet">level</param>
    public void SpawnEnemies(Planets planet)
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
                    GetTree().Root.CallDeferred("add_child", enemy);
                    //GetTree().Root.AddChild(enemy);
                }
            }
        }
    }

    /// <summary>
    /// adds enemy to current enemy for scout
    /// </summary>
    /// <param name="enemy"></param>
    public void AddEnemy(Node2D enemy)
    {
        _currentEnemies.Add(enemy);
    }

    /// <summary>
    /// checks if all the aliens are dead
    /// </summary>
    public void CheckLevelComplete()
    {
        foreach (Node2D enemy in _currentEnemies)
        {
            if (IsInstanceValid(enemy))
            {
                return;
            }
        }
        GameManager.instance.NextLevel();
    }

    /// <summary>
    /// clears current enemies and bombs for new game
    /// </summary>
    public void ClearCurrentEnemies()
    {
        if (_currentEnemies != null && _currentEnemies.Count > 0)
        {
            for (int i = _currentEnemies.Count - 1; i >= 0; i--)
            {
                if (IsInstanceValid(_currentEnemies[i]))
                {
                    _currentEnemies[i].QueueFree();
                }
            }
        }

        if (_currentEnemies != null && _currentBombs.Count > 0)
        {
            for (int i = _currentBombs.Count - 1; i >= 0; i--)
            {
                if (IsInstanceValid(_currentBombs[i]))
                {
                    _currentBombs[i].QueueFree();
                }
            }
            _currentBombs = new List<Node2D>();
        }
        
    }

    /// <summary>
    /// adds bombs so they can be referenced
    /// </summary>
    /// <param name="bomb"></param>
    public void AddBomb(Node2D bomb)
    {
        _currentBombs.Add(bomb);
    }

    /// <summary>
    /// returns singleton
    /// </summary>
    public static EnemyManager instance
    {
        get { return _instance; }
    }
}
