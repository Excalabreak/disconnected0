using Godot;
using System;

public partial class DropHitbox : Area2D
{
    [Export] private Node2D _drop;
    [Export] private int _scrapPickup;
    [Export] private float _fuelPickup;

    /// <summary>
    /// if signaled that something entered area:
    /// damage other area if it is a hitbox
    /// </summary>
    /// <param name="area">other hitbox</param>
    private void OnAreaEntered(Area2D area)
    {
        if (area is HitboxComponent)
        {
            HitboxComponent hitbox = area as HitboxComponent;

            if (hitbox.HealthComponent is PlayerResources)
            {
                PlayerResources playerResources = hitbox.HealthComponent as PlayerResources;

                playerResources.GainScrap(_scrapPickup);
                playerResources.GainFuel(_fuelPickup);

                _drop.QueueFree();
            }
        }
    }
}
