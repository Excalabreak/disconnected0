using Godot;
using System;

/*
 * Author: [Lam, Justin]
 * Last Updated: [03/17/2025]
 * [script to deploy gun]
 */

public partial class GunWeapon : BaseWeapon
{
    [Export] private PackedScene _bulletScene;
    [Export] private Node2D _playerPosition;
    [Export] private PlayerMovement _playerMovement;

    /// <summary>
    /// instatiates a gun
    /// </summary>
    protected override void Weapon()
    {
        base.Weapon();

        Laser laser = _bulletScene.Instantiate<Laser>() as Laser;
        laser.GlobalPosition = _playerPosition.GlobalPosition;

        laser.GlobalRotation = _playerPosition.GlobalRotation;
        laser.Position -= laser.Transform.Y * 10;

        GetTree().Root.AddChild(laser);
        //missile.missileMovement.SetInitialVelocity(_playerMovement.velocity);
    }
}
