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

    [Export] private PackedScene _scrapDrop;
    [Export] private PackedScene _fuelDrop;

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
            CallDeferred("Drop",_scrapDrop);
            CallDeferred("Drop",_fuelDrop);

            _asteroid.ReturnToPool();
        }
    }

    /// <summary>
    /// instantiates drops
    /// </summary>
    /// <param name="drop">what item is being droped</param>
    private void Drop(PackedScene drop)
    {
        Node2D dropNode = drop.Instantiate<Node2D>() as Node2D;
        Vector2 pos = new Vector2(
            _asteroid.GlobalPosition.X + (float)GD.RandRange(-50, 50),
            _asteroid.GlobalPosition.Y + (float)GD.RandRange(-50, 50));
        dropNode.GlobalPosition = pos;
        GetTree().Root.AddChild(dropNode);
    }
}
