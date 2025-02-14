using Godot;
using System;

/*
 * Author: [Lam, Justin]
 * Last Updated: [02/13/2025]
 * [player Movement]
 */

public partial class PlayerMovement 
{
    private Vector2 _velocity = Vector2.Zero;
    private float _thrust = 0;

    public void AcceleratePlayer(Area2D player, float delta, float acceleration)
    {
        _thrust = Input.GetActionStrength("thrust");
        _velocity += player.Transform.X * _thrust * acceleration;
        _velocity = Lerp(_velocity, Vector2.Zero, 1 * delta);
        player.Position += _velocity * delta;
    }

    /// <summary>
    /// player rotates based on input actions
    /// </summary>
    /// <param name="delta">nuber of frames since last called</param>
    public void RotatePlayer(Area2D player, float delta, float rotateSpeed)
    {
        float rot = Input.GetAxis("left", "right") * rotateSpeed;
        player.Rotate(rot * delta);
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
}
