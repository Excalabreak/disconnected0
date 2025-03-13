using Godot;
using System;

/*
 * Author: [Lam, Justin]
 * Last Updated: [03/10/2025]
 * [script to manage bombs]
 */

public partial class Bomb : Node2D
{
    [Export] private ScreenDespawn _screenDespawn;

    public override void _Process(double delta)
    {
        if (_screenDespawn != null)
        {
            _screenDespawn.CheckScreenDespawn(this);
        }
    }
}
