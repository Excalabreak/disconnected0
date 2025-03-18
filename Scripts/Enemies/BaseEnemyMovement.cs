using Godot;
using System;

/*
 * Author: [Lam, Justin]
 * Last Updated: [03/03/2025]
 * [abstract base class for enemy movement]
 */

public abstract partial class BaseEnemyMovement : Node2D
{
    [Export] protected Node2D _enemy;

    protected Vector2 _velocity;

    /// <summary>
    /// call movement
    /// </summary>
    /// <param name="delta">float of delta</param>
    public abstract void Move(float delta);

    public Vector2 velocity
    {
        get { return _velocity; }
    }
}
