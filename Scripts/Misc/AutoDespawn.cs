using Godot;
using System;

/*
 * Author: [Lam, Justin]
 * Last Updated: [03/02/2025]
 * [auto despawns after timer]
 */

public partial class AutoDespawn : Node2D
{
    [Export] private Node2D _drop;
	[Export] private Timer _timer;
	[Export] private float _despawnTime = 3f;

    public override void _Ready()
    {
        _timer.WaitTime = _despawnTime;
        _timer.OneShot = true;
        _timer.Autostart = true;
    }

    /// <summary>
    /// on timeout, queue free this item
    /// </summary>
    private void OnTimerTimeout()
    {
        _drop.QueueFree();
    }
}
