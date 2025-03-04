using Godot;
using System;

/*
 * Author: [Lam, Justin]
 * Last Updated: [03/04/2025]
 * [movement for laser]
 */

public partial class LaserMovement : Node2D
{
    [Export] private float _speed;

    //velocity
    private Vector2 _velocity = new Vector2(0,-1);

    /// <summary>
    /// move laser based on velocity
    /// </summary>
    /// <param name="delta"></param>
    public void Move(Node2D laser, float delta)
    {
        laser.Position += _velocity * _speed * delta;
    }
}
