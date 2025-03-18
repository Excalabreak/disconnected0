using Godot;
using System;

/*
 * Author: [Lam, Justin]
 * Last Updated: [02/28/2025]
 * [player script that manages all player scripts 
 * and area enter since due to tutorial bs]
 */

public partial class Player : Node2D
{
    [Export] private PlayerMovement _playerMovement;
    [Export] private BaseWeapon[] _weapons;
    [Export] private ScreenWarp _screenWarp;
    [Export] private FireEffect _fireEffect;

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        float fDelta = (float)delta;
        if (_playerMovement != null)
        {
            _playerMovement.RotatePlayer(this, fDelta);
            _playerMovement.AcceleratePlayer(this, fDelta);
        }

        if (_screenWarp != null)
        {
            _screenWarp.CheckScreenWarp(this, _playerMovement.velocity);
        }

        foreach (BaseWeapon weapon in _weapons)
        {
            weapon.WeaponCall();
        }

        if (_fireEffect != null)
        {
            _fireEffect.ShowFire();
        }
    }
}
