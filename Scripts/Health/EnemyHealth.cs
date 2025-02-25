using Godot;
using System;

/*
 * Author: [Lam, Justin]
 * Last Updated: [02/24/2025]
 * [script for enemy health]
 */

public partial class EnemyHealth : BaseHealth
{
	[Export] private int _maxHealth = 1;
	private int _currentHealth;

    public override void _Ready()
    {
        _currentHealth = _maxHealth;
    }

    public override void Damage(int damage)
    {
        
    }
}
