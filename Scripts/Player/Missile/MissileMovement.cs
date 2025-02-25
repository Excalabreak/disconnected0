using Godot;
using System;

/*
 * Author: [Lam, Justin]
 * Last Updated: [02/23/2025]
 * [Movement for missile]
 */

public partial class MissileMovement : Node
{
    //rotate speed
    [Export] private float _rotateSpeed = 3;

    //acceleration
    [Export] private float _acceleration = 20;

    private Vector2 _velocity = Vector2.Zero;

    public void AccelerateMissile(Node2D missile, float delta)
    {
        _velocity -= missile.Transform.Y * _acceleration;
        _velocity = Lerp(_velocity, Vector2.Zero, 1 * delta);
        missile.Position += _velocity * delta;
    }

    /// <summary>
    /// missile rotates based on raycasts
    /// </summary>
    /// <param name="delta">nuber of frames since last called</param>
    public void RotatePlayer(Area2D missile, float delta)
    {
        float rot = Input.GetAxis("left", "right") * _rotateSpeed;
        missile.Rotate(rot * delta);
    }

    /// <summary>
    /// function to lerp floats
    /// </summary>
    /// <param name="first">first float</param>
    /// <param name="second">second float</param>
    /// <param name="amount">how much to interpolate</param>
    /// <returns>float</returns>
    public float Lerp(float first, float second, float amount)
    {
        return first * (1 - amount) + second * amount;
    }

    /// <summary>
    /// function to lerp vector2
    /// </summary>
    /// <param name="first">first vector2</param>
    /// <param name="second">second vector2</param>
    /// <param name="amount">how much to interpolate</param>
    /// <returns>vector2</returns>
    public Vector2 Lerp(Vector2 first, Vector2 second, float amount)
    {
        float retX = Lerp(first.X, second.X, amount);
        float retY = Lerp(first.Y, second.Y, amount);
        return new Vector2(retX, retY);
    }

    public Vector2 velocity
    {
        get { return _velocity; }
    }
}
