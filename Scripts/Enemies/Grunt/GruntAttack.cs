using Godot;
using System;
using System.Reflection;

/*
 * Author: [Lam, Justin]
 * Last Updated: [03/04/2025]
 * [attack logic for grunt]
 */

public partial class GruntAttack : BaseEnemyAttack
{
    [Export] private Node2D _gruntPosition;
    [Export] private PackedScene _laser;

    [Export] private Timer _timer;
    [Export] private float _laserCooldown = 4f;

    [Export] private float _range = 20f;

    /// <summary>
    /// sets timer
    /// </summary>
    public override void _Ready()
    {
        _timer.WaitTime = _laserCooldown;
        _timer.OneShot = true;

        _timer.Start();
    }

    /// <summary>
    /// when timer times out, calls to do an attack
    /// </summary>
    private void OnTimerTimeout()
    {
        CallDeferred("Attack");

        _timer.Start();
    }

    /// <summary>
    /// function call to do attack
    /// </summary>
    public override void Attack()
    {
        Laser laser = _laser.Instantiate<Laser>() as Laser;
        laser.GlobalPosition = _gruntPosition.GlobalPosition;

        Vector2 dir = (GameManager.instance.currentPlayer.GlobalPosition +  
            new Vector2((float)GD.RandRange(-_range, _range), (float)GD.RandRange(-_range, _range))) 
            - _gruntPosition.GlobalPosition;

        laser.GlobalRotation = (-_gruntPosition.Transform.Y).AngleTo(dir);
        GetTree().Root.AddChild(laser);
    }
}
