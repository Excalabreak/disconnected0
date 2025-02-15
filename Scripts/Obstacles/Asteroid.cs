using Godot;
using System;

/*
 * Author: [Lam, Justin]
 * Last Updated: [02/15/2025]
 * [scirpt for the player]
 */

public partial class Asteroid : Area2D, IPoolItem
{
    [Export] private float _velocityRangeMin = 100f;
    [Export] private float _velocityRangeMax = 150f;

    [Export] private ScreenWarp _screenWarp;

    //velocity
    private Vector2 _velocity = Vector2.Zero;

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
        Move((float)delta);
        _screenWarp.CheckScreenWarp(this, _velocity);
    }

    /// <summary>
    /// move asteroid based on velocity
    /// </summary>
    /// <param name="delta"></param>
    private void Move(float delta)
    {
        Position += _velocity * delta;
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

        _velocity = new Vector2(
            (float)GD.RandRange(-1, 1),
            (float)GD.RandRange(-1, 1));

        _velocity = _velocity.Normalized() * (float)GD.RandRange(_velocityRangeMin, _velocityRangeMax);
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
