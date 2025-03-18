using Godot;
using System;

/*
 * Author: [Lam, Justin]
 * Last Updated: [02/24/2025]
 * [script for the asteroid's movement]
 */

public partial class AsteroidMovement : Node
{
    [Export] private float _velocityRangeMin = 100f;
    [Export] private float _velocityRangeMax = 150f;

    //velocity
    private Vector2 _velocity = Vector2.Zero;

    /// <summary>
    /// sets direction of asteroid
    /// </summary>
    public void SetDirection()
    {
        _velocity = new Vector2(
            (float)GD.RandRange(-1, 1),
            (float)GD.RandRange(-1, 1));

        _velocity = _velocity.Normalized() * (float)GD.RandRange(_velocityRangeMin, _velocityRangeMax);

    }

    /// <summary>
    /// move asteroid based on velocity
    /// </summary>
    /// <param name="delta"></param>
    public void Move(Node2D asteroid, float delta)
    {
        asteroid.Position += _velocity * delta;
    }

    /// <summary>
    /// property to get velocity
    /// </summary>
    public Vector2 velocity 
    {
        get { return _velocity; }
    }
}
