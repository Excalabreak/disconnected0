using Godot;
using System;

/*
 * Author: [Lam, Justin]
 * Last Updated: [02/24/2025]
 * [player script that manages all player scripts 
 * and area enter since due to tutorial bs]
 */

public partial class Player : Node2D
{
    [Export] private PlayerMovement _playerMovement;
    [Export] private BaseWeapon[] _weapons;
    [Export] private ScreenWarp _screenWarp;

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        float fDelta = (float)delta;
        _playerMovement.RotatePlayer(this, fDelta);
        _playerMovement.AcceleratePlayer(this, fDelta);
        _screenWarp.CheckScreenWarp(this, _playerMovement.velocity);
        foreach (BaseWeapon weapon in _weapons)
        {
            weapon.WeaponCall();
        }
    }
}
