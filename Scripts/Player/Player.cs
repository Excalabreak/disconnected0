using Godot;
using System;

/*
 * Author: [Lam, Justin]
 * Last Updated: [02/13/25]
 * [player script that manages all player scripts]
 */

public partial class Player : Area2D
{
    //player movement
    //rotate speed
    [Export] private float _rotateSpeed = 5;

    //acceleration
    [Export] private float _acceleration = 15;

    private PlayerMovement _playerMovement;

    public override void _Ready()
    {
        _playerMovement = new PlayerMovement();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        float fDelta = (float)delta;
        _playerMovement.RotatePlayer(this, fDelta, _rotateSpeed);
        _playerMovement.AcceleratePlayer(this, fDelta, _acceleration);
    }
}
