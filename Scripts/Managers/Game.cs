using Godot;
using System;

/*
 * Author: [Lam, Justin]
 * Last Updated: [02/05/2025]
 * [Manages game]
 */

public partial class Game : Node
{
    //asteroid spawn variables
	[Export] private PackedScene _asteroidScene;
	[Export] private Timer _timer;
	private bool _isSpawning = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		StartSpawningAsteroids();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
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
			Area2D asteroidInstance = _asteroidScene.Instantiate() as Area2D;
            asteroidInstance.GlobalPosition = spawnloc;
            AddChild(asteroidInstance);
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
