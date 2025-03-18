using Godot;
using System;

/*
 * Author: [Lam, Justin]
 * Last Updated: [03/09/2025]
 * [State when scout moves in a random direction
 * for a set amount of time]
 */

public partial class ScoutWanderState : State
{
    [Export] private ScoutMovement _scoutMovement;
    
    [Export] private Sprite2D _lightSprite;
    [Export] private Color _wanderColor;

    [Export] private Timer _wanderTimer;
    [Export] private float _wanderTime = 5f;

    /// <summary>
    /// on Ready, sets timer
    /// </summary>
    public override void Ready()
    {
        _wanderTimer.WaitTime = _wanderTime;
        _wanderTimer.OneShot = true;
    }

    /// <summary>
    /// on enter, 
    /// the scout will set the direction of move
    /// change light to indicate it's just moving
    /// starts the timer;
    /// </summary>
    public override void Enter()
    {
        _scoutMovement.SetVelocity(
            new Vector2(
                (float)GD.RandRange(-1, 1),
                (float)GD.RandRange(-1, 1)).Normalized());

        _lightSprite.Modulate = _wanderColor;
        _wanderTimer.Start();
    }

    /// <summary>
    /// on exit,
    /// set velocity to zero
    /// stop wander timer
    /// </summary>
    public override void Exit()
    {
        _scoutMovement.SetVelocity(Vector2.Zero);
        _wanderTimer.Stop();
    }

    private void OnTimerTimeout()
    {
        sm.TransitionTo("ScanningState");
    }
}
