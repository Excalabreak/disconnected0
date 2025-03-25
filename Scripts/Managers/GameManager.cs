using Godot;
using System;

/*
 * Author: [Lam, Justin]
 * Last Updated: [03/21/2025]
 * [Game manager for game]
 */

public partial class GameManager : Node
{
    private static GameManager _instance;

    [Export] private AsteroidPool _asteroidPool;
    [Export] private Node2D _currentPlayer;
    private Planets _currentLevel = Planets.PLUTO;

    [Export] private bool _testEnemy = false;

    private bool _isPlaying = false;

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

        if (_testEnemy)
        {
            GD.Print("test new game is still here");
            NewGame();
        }
    }

    public override void _Process(double delta)
    {
        if (_isPlaying)
        {
            EnemyManager.instance.CheckLevelComplete();
        }
    }

    /// <summary>
    /// resets the game, and loads first level
    /// </summary>
    public void NewGame()
    {
        _currentLevel = Planets.PLUTO;

        _asteroidPool.StopSpawningAsteroids();
        _asteroidPool.ClearAsteroids();
        
        EnemyManager.instance.ClearCurrentEnemies();
        LoadLevel(_currentLevel);
        _asteroidPool.StartSpawningAsteroids();
    }

    /// <summary>
    /// check if the game is won, if not load next level
    /// </summary>
    /// <param name="planet">level</param>
    public void NextLevel()
    {
        if (_currentLevel != Planets.EARTH)
        {
            _currentLevel = _currentLevel + 1;
            LoadLevel(_currentLevel);
        }
    }

    /// <summary>
    /// loads a level
    /// </summary>
    /// <param name="planet">level</param>
    public void LoadLevel(Planets planet)
    {
        EnemyManager.instance.SpawnEnemies(planet);
        UIManager.instance.SetBackgroundPlanet(planet);
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
