using Godot;
using System;
using System.Reflection;

/*
 * Author: [Lam, Justin]
 * Last Updated: [03/06/2025]
 * [state for when the scouts calls in grunts]
 */

public partial class ScoutDeployState : State
{
    [Export] private PackedScene _grunt;

    [Export] private Sprite2D _lightSprite;
    [Export] private Color _deployColor;

    [Export] private Sprite2D _signalSprite;

    [Export] private Timer _deployTimer;
    [Export] private float _deployTime = 1f;

    [Export] private Vector2 _gruntSpawnLoc; 

    /// <summary>
    /// sets timer
    /// </summary>
    public override void Ready()
    {
        _signalSprite.Visible = false;

        _deployTimer.WaitTime = _deployTime;
        _deployTimer.OneShot = true;
    }

    public override void Enter()
    {
        _lightSprite.Modulate = _deployColor;

        _signalSprite.Visible = true;

        Node2D grunt = _grunt.Instantiate<Node2D>() as Node2D;
        grunt.GlobalPosition = _gruntSpawnLoc;
        EnemyManager.instance.AddEnemy(grunt);
        GetTree().Root.AddChild(grunt);

        _deployTimer.Start();
    }

    /// <summary>
    /// makes sure timer stopped
    /// </summary>
    public override void Exit()
    {
        _signalSprite.Visible = false;

        _deployTimer.Stop();
    }

    /// <summary>
    /// after timer, go back to wandering
    /// </summary>
    private void OnTimerTimeout()
    {
        sm.TransitionTo("WanderState");
    }
}
