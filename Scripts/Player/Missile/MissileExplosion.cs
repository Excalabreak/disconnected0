using Godot;
using System;

/*
 * Author: [Lam, Justin]
 * Last Updated: [03/10/2025]
 * [script for missile to explode]
 */

public partial class MissileExplosion : Node
{
    [Export] private Node2D _missile;
    [Export] private PackedScene _explosion;

    /// <summary>
    /// creates explosion
    /// </summary>
    public void AddExplosion()
    {
        Node2D explosion = _explosion.Instantiate<Node2D>() as Node2D;
        explosion.GlobalPosition = _missile.GlobalPosition;
        explosion.GlobalRotation = _missile.GlobalRotation;
        GetTree().Root.AddChild(explosion);
        _missile.QueueFree();
    }
}
