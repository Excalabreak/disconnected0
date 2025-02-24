using Godot;
using System;

/*
 * Author: [Lam, Justin]
 * Last Updated: [02/24/2025]
 * [scirpt for the asteroid and pooling]
 */

public partial class Asteroid : Area2D, IPoolItem
{
    [Export] private AsteroidMovement _asteroidMovement;
    [Export] private ScreenWarp _screenWarp;

    public bool active { get; set; } = false;

    /// <summary>
    /// on start, set velocity
    /// </summary>
    public override void _Ready()
    {
        Visible = false;
        Monitoring = false;
        Monitorable = false;
        SetProcess(false);
    }

    /// <summary>
    /// call function every frame
    /// </summary>
    /// <param name="delta"></param>
    public override void _Process(double delta)
    {
        _asteroidMovement.Move(this, (float)delta);
        _screenWarp.CheckScreenWarp(this, _asteroidMovement.velocity);
    }

    /// <summary>
    /// when spawned for object pooling
    /// </summary>
    public void SpawnFromPool()
    {
        active = true;
        Visible = true;
        Monitoring = true;
        Monitorable = true;
        SetProcess(true);

        _asteroidMovement.SetDirection();
    }

    /// <summary>
    /// deactivates the object for pooling
    /// </summary>
    public void ReturnToPool()
    {
        active = false;
        Visible = false;
        SetDeferred(Area2D.PropertyName.Monitoring, false);
        SetDeferred(Area2D.PropertyName.Monitorable, false);
        SetProcess(false);
    }
}
