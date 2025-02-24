using Godot;
using System;
using System.Diagnostics;

public partial class MissileWeapon : BaseWeapon
{
    [Export] private PackedScene _missileScene;
    [Export] private Area2D _playerPosition;

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
        GD.Print("missile");
        Missile missile = _missileScene.Instantiate<Missile>() as Missile;
        missile.GlobalPosition = _playerPosition.GlobalPosition;
        missile.GlobalRotation = _playerPosition.GlobalRotation;
        GetTree().Root.AddChild(missile);
    }
}
