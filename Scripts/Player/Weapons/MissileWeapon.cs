using Godot;
using System;
using System.Diagnostics;

public partial class MissileWeapon : BaseWeapon
{
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
    }
}
