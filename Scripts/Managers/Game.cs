using Godot;
using System;

/*
 * Author: [Lam, Justin]
 * Last Updated: [02/05/2025]
 * [Manages game]
 */

public partial class Game : Node
{
	[Export] private float _asteroidSpawnRate = 1f;
	[Export] private PackedScene _asteroidScene;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Area2D asteroidInstance = _asteroidScene.Instantiate() as Area2D;
		asteroidInstance.GlobalPosition = new Vector2(500, 300);
		AddChild(asteroidInstance);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
