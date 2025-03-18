using Godot;
using System;

/*
 * Author: [Lam, Justin]
 * Last Updated: [03/03/2025]
 * [script for enemy health]
 */

public partial class EnemyHealth : BaseHealth
{
    [Export] private Node2D _enemy;
	[Export] private int _maxHealth = 1;
    private int _currentHealth;

    [Export] private PackedScene _scrapDrop;
    [Export] private PackedScene _fuelDrop;
    [Export] private int _numOfScrapDrop = 0;
    [Export] private int _numOfFuelDrop = 0;

    public override void _Ready()
    {
        _currentHealth = _maxHealth;
    }

    /// <summary>
    /// damages enemies when called
    /// </summary>
    /// <param name="damage"></param>
    public override void Damage(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            if (_numOfScrapDrop > 0)
            {
                for (int i = 0; i < _numOfScrapDrop; i++)
                {
                    CallDeferred("Drop", _scrapDrop);
                }
            }
            if (_numOfFuelDrop > 0)
            {
                for (int i = 0; i < _numOfFuelDrop; i++)
                {
                    CallDeferred("Drop", _fuelDrop);
                }
            }

            _enemy.QueueFree();
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
            _enemy.GlobalPosition.X + (float)GD.RandRange(-50, 50),
            _enemy.GlobalPosition.Y + (float)GD.RandRange(-50, 50));
        dropNode.GlobalPosition = pos;
        GetTree().Root.AddChild(dropNode);
    }
}
