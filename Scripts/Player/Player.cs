using Godot;
using System;

/*
 * Author: [Lam, Justin]
 * Last Updated: [02/15/25]
 * [player script that manages all player scripts]
 */

public partial class Player : Area2D
{
    [Export] private PlayerMovement _playerMovement;
    [Export] private ScreenWarp _screenWarp;

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        float fDelta = (float)delta;
        _playerMovement.RotatePlayer(this, fDelta);
        _playerMovement.AcceleratePlayer(this, fDelta);
        _screenWarp.CheckScreenWarp(this, _playerMovement.velocity);
    }
}
