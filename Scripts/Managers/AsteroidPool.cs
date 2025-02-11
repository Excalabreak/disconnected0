using Godot;
using System;
using System.Collections.Generic;

/*
 * Author: [Lam, Justin]
 * Last Updated: [02/10/2025]
 * [spawner and object pool for the asteroid]
 */

public partial class AsteroidPool : Node2D
{
    //asteroid spawn variables
    [Export] private PackedScene _asteroidScene;
    [Export] private Timer _timer;
    private bool _isSpawning = false;

    //object pooling
    [Export(PropertyHint.Range, "1,100,1,or_greater")]
    public int initialSpawn = 10;
    private List<Asteroid> _pooledItems;

    /// <summary>
    /// init object pool
    /// </summary>
    public override void _Ready()
    {
        _pooledItems = new List<Asteroid>();
        for (int i = 0; i < initialSpawn; i++)
        {
            Asteroid asteroid = _asteroidScene.Instantiate<Asteroid>() as Asteroid;
            _pooledItems.Add(asteroid);
            GD.Print("init");
            AddChild(asteroid);
        }
        //test, remove later
        StartSpawningAsteroids();
    }

    /// <summary>
    /// gets a pooled item or instantiate a new on if needed
    /// </summary>
    /// <returns></returns>
    private Asteroid GetAsteroid()
    {
        for (int i = 0; i < _pooledItems.Count; i++)
        {
            if (!_pooledItems[i].active)
            {
                GD.Print("Gotten");
                return _pooledItems[i];
            }
        }

        Asteroid asteroid = _asteroidScene.Instantiate<Asteroid>() as Asteroid;
        _pooledItems.Add(asteroid);
        AddChild(asteroid);
        GD.Print("Created");
        return asteroid;

    }

    /// <summary>
    /// spawns asteroid when timer runs out
    /// </summary>
    private void OnTimerTimeout()
    {
        if (_isSpawning)
        {
            SpawnAsteroid(2);
            _timer.Start();
        }
    }

    /// <summary>
    /// spawns asteroids
    /// </summary>
    /// <param name="amount">amount of asteroids spawn</param>
    private void SpawnAsteroid(int amount)
    {
        Vector2 resolution = GetViewport().GetVisibleRect().Size;

        for (int i = 0; i < amount; i++)
        {
            //finds random spawn location outside of screen
            int loc = GD.RandRange(0, 3);
            Vector2 spawnloc = Vector2.Zero;
            switch (loc)
            {
                case 0:
                    spawnloc = new Vector2(resolution.X + 50f, (float)GD.RandRange(0f, resolution.Y));
                    break;
                case 1:
                    spawnloc = new Vector2((float)GD.RandRange(0f, resolution.X), resolution.Y + 50f);
                    break;
                case 2:
                    spawnloc = new Vector2(-50f, (float)GD.RandRange(0f, resolution.Y));
                    break;
                case 3:
                    spawnloc = new Vector2((float)GD.RandRange(0f, resolution.Y), -50f);
                    break;
                default:
                    break;
            }

            //spawns asteroids
            Asteroid asteroidInstance = GetAsteroid() as Asteroid;
            asteroidInstance.SpawnFromPool();
            asteroidInstance.GlobalPosition = spawnloc;
        }
    }

    /// <summary>
    /// swiches bool to start spawning asteroids
	/// also spawns the first asteroids
    /// </summary>
    private void StartSpawningAsteroids()
    {
        _timer.Start();
        SpawnAsteroid(3);
        _isSpawning = true;
    }

    /// <summary>
    /// swiches bool to stop spawning asteroids
    /// </summary>
    private void StopSpawningAsteroids()
    {
        _timer.Stop();
        _isSpawning = false;
    }
}

/// <summary>
/// interface for pool
/// </summary>
public interface IPoolItem
{
    public bool active { get; set; }
}
