using Godot;
using System;

/*
 * Author: [Lam, Justin]
 * Last Updated: [03/05/2025]
 * [Scout Movement pattern]
 */

public partial class ScoutMovement : BaseEnemyMovement
{
    [Export] private float _speed = 70f;

    /// <summary>
    /// calls to move
    /// </summary>
    /// <param name="delta"></param>
    public override void Move(float delta)
    {
        _enemy.Position += _velocity * _speed * delta;
    }

    /// <summary>
    /// sets the direction of velocity
    /// </summary>
    /// <param name="dir">direction</param>
    public void SetVelocity(Vector2 dir)
    {
        _velocity = dir.Normalized();
    }
}
