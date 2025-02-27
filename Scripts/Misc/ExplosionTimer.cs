using Godot;
using System;

/*
 * Author: [Lam, Justin]
 * Last Updated: [02/25/2025]
 * [timer for explosions]
 */

public partial class ExplosionTimer : Timer
{
	[Export] private Node2D _explosion;

	private void OnTimeout()
	{
		_explosion.QueueFree();
	}
}
