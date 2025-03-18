using Godot;
using System;

/*
 * Author: [Lam, Justin]
 * Last Updated: [03/10/2025]
 * [script to deploy bombs]
 */

public partial class BommerAttack : BaseEnemyAttack
{
    [Export] private Node2D _enemy;
    [Export] private PackedScene _bombs;
    [Export] private float _deployDis = 75f;

    [Export] private Timer _timer;
    [Export] private float _deployTime = 4f;

    public override void _Ready()
    {
        base._Ready();

        _timer.WaitTime = _deployTime;
        _timer.OneShot = true;
        _timer.Autostart = false;

        _timer.Start();
    }

    /// <summary>
    /// thinking now, i dont need a base enemy attack... oh well
    /// instantiates a bomb
    /// </summary>
    public override void Attack()
    {
        Node2D dropNode = _bombs.Instantiate<Node2D>() as Node2D;
        Vector2 pos = new Vector2(
            _enemy.GlobalPosition.X + (float)GD.RandRange(-_deployDis, _deployDis),
            _enemy.GlobalPosition.Y + (float)GD.RandRange(-_deployDis, _deployDis));
        dropNode.GlobalPosition = pos;
        GetTree().Root.AddChild(dropNode);
    }

    /// <summary>
    /// on time out:
    /// instantiate a bomb
    /// </summary>
    private void OnTimerTimeout()
    {
        Attack();
        _timer.Start();
    }
}
