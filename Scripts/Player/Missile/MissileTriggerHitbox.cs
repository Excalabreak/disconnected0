using Godot;
using System;
using System.Reflection;

/*
 * Author: [Lam, Justin]
 * Last Updated: [02/26/2025]
 * [Hitbox for missile buh]
 */

public partial class MissileTriggerHitbox : Area2D
{
    [Export] private MissileExplosion _missileExplosion;
    [Export] private int _onHitDamage = 1;

    /// <summary>
    /// hitbox for missle to damage whatever it hits
    /// </summary>
    /// <param name="area">other</param>
    private void OnAreaEntered(Area2D area)
    {
        if (area is HitboxComponent)
        {
            HitboxComponent hitbox = area as HitboxComponent;

            hitbox.Damage(_onHitDamage);

            //explosion
            _missileExplosion.CallDeferred("AddExplosion");
        }
    }

    private void OnArmed()
    {
        SetDeferred(Area2D.PropertyName.Monitoring, true);
        SetDeferred(Area2D.PropertyName.Monitorable, true);
    }
}
