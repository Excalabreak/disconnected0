using Godot;
using System;

/*
 * Author: [Lam, Justin]
 * Last Updated: [03/09/2025]
 * [script for bomber movement]
 */

public partial class BomberMovement : BaseEnemyMovement
{
    [Export] private BomberEnemyScript _bomberEnemyScript;

    [Export] private float _runSpeed = 120f;

    [Export] private float _deploySpeed = 60f;
    [Export] private Timer _deployTimer;
    [Export] private float _deployDirTime;
    private Vector2 _deployDir;

    public override void _Ready()
    {
        base._Ready();

        _deployTimer.WaitTime = _deployDirTime;
        _deployTimer.Autostart = false;
        _deployTimer.OneShot = true;

        OnTimerTimeout();
        _deployTimer.Start();
    }

    /// <summary>
    /// bomber constantly runs away from player
    /// </summary>
    /// <param name="delta"></param>
    public override void Move(float delta)
    {
        if (_bomberEnemyScript.InRun)
        {
            //runs away
            _velocity = -(_enemy.GlobalPosition.DirectionTo(GameManager.instance.currentPlayer.GlobalPosition));
            _enemy.Position += _velocity * _runSpeed * delta;
        }
        else
        {
            if (GD.RandRange(0, 100) < 5)
            {
                _velocity = _deployDir;
            }
            _enemy.Position += _velocity * _deploySpeed * delta;
        }
    }

    /// <summary>
    /// when the timer times out
    /// set the _deploy dir to random
    /// </summary>
    private void OnTimerTimeout()
    {
        _deployDir = new Vector2(
            (float)GD.RandRange(-1, 1),
            (float)GD.RandRange(-1, 1)).Normalized();
        _deployTimer.Start();
    }
}
