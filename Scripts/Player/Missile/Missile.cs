using Godot;
using System;

/*
 * Author: [Lam, Justin]
 * Last Updated: [02/28/2025]
 * [manager for missile]
 */

public partial class Missile : Node2D
{
    [Export] private MissileMovement _missileMovement;
    [Export] private ScreenWarp _screenWarp;

    public override void _Process(double delta)
    {
        float fDelta = (float)delta;
        _missileMovement.AccelerateMissile(this, fDelta);
        _missileMovement.RotateTwoardsTarget(this, fDelta);
        _screenWarp.CheckScreenWarp(this, _missileMovement.velocity);
    }

    public MissileMovement missileMovement
    {
        get { return _missileMovement; }
    }
}
