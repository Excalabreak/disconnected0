using Godot;
using System;

/*
 * Author: [Lam, Justin]
 * Last Updated: [03/04/2025]
 * [script for areas that are only hitboxes]
 */

public partial class HurtBox : Area2D
{
    [Export] private Node2D _attack;
    [Export] private int _onHitDamage = 1;

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

            hitbox.Damage(_onHitDamage);
            _attack.QueueFree();
        }
    }
}
