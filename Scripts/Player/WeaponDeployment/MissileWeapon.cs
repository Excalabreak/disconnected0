using Godot;
using System;
using System.Diagnostics;

public partial class MissileWeapon : BaseWeapon
{
    [Export] private PackedScene _missileScene;
    [Export] private Node2D _playerPosition;
    [Export] private PlayerMovement _playerMovement;

    public override void WeaponCall()
    {
        if (Input.IsActionJustPressed("missile"))
        {
            base.WeaponCall();
        }
    }

    protected override void Weapon()
    {
        base.Weapon();
        Missile missile = _missileScene.Instantiate<Missile>() as Missile;
        missile.GlobalPosition = _playerPosition.GlobalPosition;
        missile.GlobalRotation = _playerPosition.GlobalRotation;
        missile.missileMovement.SetInitialVelocity(_playerMovement.velocity);
        GetTree().Root.AddChild(missile);
    }
}
