using Godot;
using System;

public partial class MissileTriggerHitbox : Area2D
{
    [Export] private Missile _missile;
    [Export] private int _onHitDamage = 1;

    private void OnAreaEntered(Area2D area)
    {
        if (area is HitboxComponent)
        {
            HitboxComponent hitbox = area as HitboxComponent;

            hitbox.Damage(_onHitDamage);

            //explosion

            _missile.QueueFree();
        }
    }

    private void OnArmed()
    {
        Monitoring = true;
        Monitorable = true;
    }
}
