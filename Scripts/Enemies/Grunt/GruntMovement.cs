using Godot;
using System;

/*
 * Author: [Lam, Justin]
 * Last Updated: [03/03/2025]
 * [script for grunt movement]
 */

public partial class GruntMovement : BaseEnemyMovement
{
    [Export] private float _speed = 75f;

    [Export] private Timer _timer;
    [Export] private float _changeAltitudeTime = 5f;

    /// <summary>
    /// on ready, sets direction
    /// </summary>
    public override void _Ready()
    {
        _velocity = new Vector2(_speed, 0);
        if (GD.RandRange(0, 1) == 0)
        {
            _velocity.X = -_velocity.X;
        }

        _timer.WaitTime = _changeAltitudeTime;
        _timer.OneShot = true;
        _timer.Start();
    }

    /// <summary>
    /// called to move grunt
    /// </summary>
    /// <param name="delta">time between frames</param>
    public override void Move(float delta)
    {
        _enemy.Position += _velocity * delta;
    }

    private void OnTimerTimeout()
    {
        int rand = GD.RandRange(0, 2);

        switch (rand)
        {
            case 0:
                _velocity.Y = _speed;
                break;
            case 1:
                _velocity.Y = 0;
                break;
            case 2:
                _velocity.Y = -_speed;
                break;

            default:
                break;
        }

        _timer.Start();
    }
}
