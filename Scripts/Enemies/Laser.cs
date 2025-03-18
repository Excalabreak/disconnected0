using Godot;
using System;

/*
 * Author: [Lam, Justin]
 * Last Updated: [03/04/2025]
 * [script for laser]
 */

public partial class Laser : Node2D
{
	[Export] private LaserMovement _laserMovement;
	[Export] private ScreenDespawn _screenDespawn;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		float fDelta = (float)delta;
		if (_laserMovement != null)
		{
			_laserMovement.Move(this, fDelta);
		}

        if (_screenDespawn != null)
        {
			_screenDespawn.CheckScreenDespawn(this);
        }
    }
}
