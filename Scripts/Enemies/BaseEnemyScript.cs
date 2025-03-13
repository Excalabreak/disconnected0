using Godot;
using System;

/*
 * Author: [Lam, Justin]
 * Last Updated: [03/03/2025]
 * [base class for enemies]
 */

public partial class BaseEnemyScript : Node2D
{
	[Export] protected ScreenWarp _screenWarp;
	[Export] protected BaseEnemyMovement _enemyMovement;

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
        float fDelta = (float)delta;

		if (_enemyMovement != null)
		{
			_enemyMovement.Move(fDelta);
		}

        if (_screenWarp != null)
        {
            _screenWarp.CheckScreenWarp(this, _enemyMovement.velocity);
        }
    }
}
