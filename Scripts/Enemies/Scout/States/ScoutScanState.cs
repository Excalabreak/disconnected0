using Godot;
using System;
using System.Diagnostics;

/*
 * Author: [Lam, Justin]
 * Last Updated: [03/05/2025]
 * [state for when the scout scans for the player]
 */

public partial class ScoutScanState : State
{
    [Export] private Sprite2D[] _signalSprites;

    [Export] private Area2D _scanArea;

    [Export] private Sprite2D _lightSprite;
    [Export] private Color _scoutColor;

    [Export] private Timer _scanTimer;
    [Export] private float _scanTime = 3f;

    /// <summary>
    /// on ready
    /// set scanner to false
    /// set timer
    /// </summary>
    public override void Ready()
    {
        SetScanner(false);

        _scanTimer.WaitTime = _scanTime;
        _scanTimer.OneShot = true;
    }

    /// <summary>
    /// on enter:
    /// set color of sprite
    /// activate scanner
    /// start scan timer
    /// </summary>
    public override void Enter()
    {
        _lightSprite.Modulate = _scoutColor;

        SetScanner(true);

        _scanTimer.Start();
    }

    /// <summary>
    /// when exiting state
    /// set scanner to false
    /// stop timer
    /// </summary>
    public override void Exit()
    {
        SetScanner(false);

        _scanTimer.Stop();
    }

    /// <summary>
    /// sets the scanner on or off
    /// </summary>
    /// <param name="isScanning">is the scanner on</param>
    private void SetScanner(bool isScanning)
    {
        foreach (Sprite2D sprite in _signalSprites)
        {
            sprite.Visible = isScanning;
        }

        if (isScanning)
        {
            _scanArea.Monitoring = true;
        }
        else
        {
            _scanArea.SetDeferred(Area2D.PropertyName.Monitoring, isScanning);
        }
    }

    /// <summary>
    /// if timer times out
    /// go back to wander state
    /// </summary>
    private void OnTimerTimeout()
    {
        sm.TransitionTo("WanderState");
    }

    /// <summary>
    /// if player is in area,
    /// go to deploy state
    /// </summary>
    /// <param name="area"></param>
    private void OnAreaEntered(Area2D area)
    {
        //switch state to deploy
        GD.Print("deploy");
    }
}
