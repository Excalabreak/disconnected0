using Godot;
using System;

/*
 * Author: [Lam, Justin]
 * Last Updated: [02/28/2025]
 * [simple script for fire effect]
 */

public partial class FireEffect : Node2D
{
	[Export] private Sprite2D _fireSprite;
	[Export] private PlayerMovement _playerMovement;

	/// <summary>
	/// shows fire if thrusting
	/// </summary>
	public void ShowFire()
	{
        if (!_fireSprite.Visible && _playerMovement.thrust > 0)
        {
			_fireSprite.Visible = true;
        }
		else if (_fireSprite.Visible && _playerMovement.thrust <= 0)
		{
            _fireSprite.Visible = false;
        }
    }
}
