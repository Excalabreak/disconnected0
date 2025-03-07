using Godot;
using System;

/*
 * Author: [Lam, Justin]
 * Last Updated: [03/07/2025]
 * [movement script for escort]
 */

public partial class EscortMovement : BaseEnemyMovement
{
    [Export] private float _speed = 75f;
    [Export] private float _minDistance = 75f;


    /// <summary>
    /// the escort tries to get in range of the player w/o contact
    /// 
    /// </summary>
    /// <param name="delta"></param>
    public override void Move(float delta)
    {
        if (!PlayerTooClose())
        {
            _velocity = _enemy.GlobalPosition.DirectionTo(GameManager.instance.currentPlayer.GlobalPosition) * _speed;
            _enemy.Position += _velocity * delta;
        }
    }

    /// <summary>
    /// gets this escort and player distance and sees if it's too close
    /// </summary>
    /// <returns>true if player is within min distance</returns>
    private bool PlayerTooClose()
    {
        return (_enemy.GlobalPosition.DistanceTo(
            GameManager.instance.currentPlayer.GlobalPosition)
            < _minDistance);
    }
}
