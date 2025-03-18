using Godot;
using System;

/*
 * Author: [Lam, Justin]
 * Last Updated: [03/04/2025]
 * [manager for missile]
 */

public partial class Missile : Node2D
{
    [Export] private MissileMovement _missileMovement;
    [Export] private MissileDetection _missileDetection;
    [Export] private ScreenWarp _screenWarp;

    public override void _Process(double delta)
    {
        float fDelta = (float)delta;

        if (_missileMovement != null)
        {
            _missileMovement.AccelerateMissile(this, fDelta);
            _missileMovement.RotateTwoardsTarget(this, fDelta);
        }

        if (_screenWarp != null)
        {
            _screenWarp.CheckScreenWarp(this, _missileMovement.velocity);
        }

        if (_missileDetection != null)
        {
            _missileDetection.CheckDetection();
        }
    }

    public MissileMovement missileMovement
    {
        get { return _missileMovement; }
    }
}
