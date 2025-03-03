using Godot;
using System;

/*
 * Author: [Lam, Justin]
 * Last Updated: [03/01/2025]
 * [component for hitboxes]
 */

public partial class HitboxComponent : Area2D
{
	[Export] private BaseHealth _healthComponent;
	[Export] private int _onHitDamage = 1;

	/// <summary>
	/// gives a hitbox for damage 
	/// </summary>
	public void Damage(int damage)
	{
		if (_healthComponent != null)
		{
			_healthComponent.Damage(damage);
		}
	}

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
		}
	}

	public BaseHealth HealthComponent
	{
		get { return _healthComponent; }
	}
}
