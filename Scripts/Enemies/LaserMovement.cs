using Godot;
using System;
using System.Reflection.Emit;

/*
 * Author: [Lam, Justin]
 * Last Updated: [03/04/2025]
 * [movement for laser]
 */

public partial class LaserMovement : Node2D
{
    [Export] private Node2D _laser;
    [Export] private float _speed;


    //velocity
    private Vector2 _velocity = Vector2.Zero;

    public override void _Ready()
    {
        _velocity -= _laser.Transform.Y * _speed;
    }

    /// <summary>
    /// move laser based on velocity
    /// </summary>
    /// <param name="delta"></param>
    public void Move(Node2D laser, float delta)
    {
        laser.Position += _velocity * delta; ;
    }
}
