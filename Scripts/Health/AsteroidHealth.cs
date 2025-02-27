using Godot;
using System;

/*
 * Author: [Lam, Justin]
 * Last Updated: [02/24/2025]
 * [script for asteroid health due to object pooling]
 */

public partial class AsteroidHealth : BaseHealth
{
    [Export] private Asteroid _asteroid;
    [Export] private int _maxHealth = 1;
    private int _currentHealth;

    /// <summary>
    /// sets the _current Health to max health
    /// </summary>
    public override void _Ready()
    {
        _currentHealth = _maxHealth;
    }

    /// <summary>
    /// when damaged, remove from health
    /// when current health is less than zero, return to pool
    /// </summary>
    /// <param name="damage"></param>
    public override void Damage(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            _asteroid.ReturnToPool();
        }
    }
}
