using Godot;
using System;

/*
 * Author: [Lam, Justin]
 * Last Updated: [02/26/2025]
 * [class for missile to wait before being able to explode]
 */

public partial class MissileEngage : Node
{
    [Export] private MissileExplosion _missileExplosion;
    [Export] private Sprite2D _armingLightSprite;
	[Export] private Color _armingColor = Color.Color8(0, 1, 1, 1);
    [Export] private Color _armedColor = Color.Color8(0, 1, 0, 1);
	[Export] private Color _lockedOnColor = Color.Color8(1, 0, 0, 1);

	[Export] private Timer _armingTimer;
	[Export] private float _armingTime = 2f;
    [Signal] public delegate void ArmedEventHandler();

	[Export] private Timer _selfDestructTimer;
    [Export] private float _selfDestructTime = 5f;

    /// <summary>
    /// makes sure sprites and timers are set up correctly
    /// </summary>
    public override void _Ready()
    {
        _armingLightSprite.Modulate = _armingColor;

        _armingTimer.OneShot = true;
        _armingTimer.WaitTime = _armingTime;

        _selfDestructTimer.OneShot = true;
        _selfDestructTimer.WaitTime = _selfDestructTime;

        _armingTimer.Start();
    }

    /// <summary>
    /// switches _armed to true
    /// </summary>
    private void OnArmingTimerTimeout()
    {
        _armingLightSprite.Modulate = _armedColor;
        EmitSignal("Armed");
        _selfDestructTimer.Start();
    }
    
    /// <summary>
    /// self destructs
    /// </summary>
    private void OnSelfDestructTimerTimeout()
    {
        _missileExplosion.CallDeferred("AddExplosion");
    }

    /// <summary>
    /// changes the arming light to locked on
    /// </summary>
    public void LockedOnLight()
    {
        _armingLightSprite.Modulate = _lockedOnColor;
    }
}
