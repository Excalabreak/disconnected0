using Godot;
using System;
using System.Reflection;

/*
 * Author: [Lam, Justin]
 * Last Updated: [03/11/2025]
 * [script for multirole movement]
 */

public partial class MultiroleMovement : BaseEnemyMovement
{
    [Export] private float _acceleration = 8f;
    [Export] private float _rotateSpeed = 4f;

    /// <summary>
    /// calls all the move functions
    /// </summary>
    /// <param name="delta">time between frames</param>
    public override void Move(float delta)
    {
        AccelerateEnemy(delta);
        RotateTwoardsTarget(delta);
    }

    /// <summary>
    /// accelerates enemy
    /// </summary>
    /// <param name="delta">time between frames</param>
    private void AccelerateEnemy(float delta)
    {
        _velocity -= _enemy.Transform.Y * _acceleration;
        _velocity = Lerp(_velocity, Vector2.Zero, 1 * delta);
        _enemy.Position += _velocity * delta;
    }

    /// <summary>
    /// rotates enemy to player
    /// </summary>
    /// <param name="delta">time between frames</param>
    public void RotateTwoardsTarget(float delta)
    {
        Vector2 dir = GameManager.instance.currentPlayer.GlobalPosition - _enemy.GlobalPosition;
        float angleTo = _enemy.Transform.Y.AngleTo(dir);
        _enemy.Rotate(-(Mathf.Sign(angleTo) * Mathf.Min(delta * _rotateSpeed, Mathf.Abs(angleTo))));
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
