using Godot;
using System;

/*
 * Author: [Lam, Justin]
 * Last Updated: [1/30/25]
 * [scirpt for the player]
 */

public partial class Asteroid : Area2D
{
    [Export] private float _velocityRange = 150f;
    [Export] private float _screenWarpBuffer = 50f;

    //velocity
    private Vector2 _velocity = Vector2.Zero;

    /// <summary>
    /// on start, set velocity
    /// </summary>
    public override void _Ready()
    {
        _velocity = new Vector2(
            (float)GD.RandRange(-_velocityRange, _velocityRange),
            (float)GD.RandRange(-_velocityRange, _velocityRange));
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
}
