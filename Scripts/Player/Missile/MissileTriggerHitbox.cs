using Godot;
using System;
using System.Reflection;

/*
 * Author: [Lam, Justin]
 * Last Updated: [02/25/2025]
 * [Hitbox for missile buh]
 */

public partial class MissileTriggerHitbox : Area2D
{
    [Export] private Missile _missile;
    [Export] private PackedScene _explosion;
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
            CallDeferred("AddExplosion");

            _missile.QueueFree();
        }
    }

    /// <summary>
    /// creates explosion
    /// </summary>
    private void AddExplosion()
    {
        Node2D explosion = _explosion.Instantiate<Node2D>() as Node2D;
        explosion.GlobalPosition = _missile.GlobalPosition;
        explosion.GlobalRotation = _missile.GlobalRotation;
        GetTree().Root.AddChild(explosion);
    }

    private void OnArmed()
    {
        SetDeferred(Area2D.PropertyName.Monitoring, true);
        SetDeferred(Area2D.PropertyName.Monitorable, true);
    }
}
