using Godot;
using System;

/*
 * Author: [Lam, Justin]
 * Last Updated: [02/10/2025]
 * [scirpt for the player]
 */

public partial class Asteroid : Area2D, IPoolItem
{
    [Export] private float _velocityRangeMin = 100f;
    [Export] private float _velocityRangeMax = 150f;
    [Export] private float _screenWarpBuffer = 50f;

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
        CheckScreenWarp();
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
    /// checks if asteroid is offscreen and warps them to the correct location
    /// </summary>
    private void CheckScreenWarp()
    {
        Vector2 resolution = GetViewport().GetVisibleRect().Size;

        if (Position.X < -_screenWarpBuffer && _velocity.X < 0)
        {
            Position = new Vector2(resolution.X + _screenWarpBuffer, Position.Y);
        }
        if (Position.Y < -_screenWarpBuffer && _velocity.Y < 0)
        {
            Position = new Vector2(Position.X, resolution.Y + _screenWarpBuffer);
        }
        if (Position.X > resolution.X + _screenWarpBuffer && _velocity.X > 0)
        {
            Position = new Vector2(-_screenWarpBuffer, Position.Y);
        }
        if (Position.Y > resolution.Y + _screenWarpBuffer && _velocity.Y > 0)
        {
            Position = new Vector2(Position.X, -_screenWarpBuffer);
        }
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
