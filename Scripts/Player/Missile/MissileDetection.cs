using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

/*
 * Author: [Lam, Justin]
 * Last Updated: [02/26/2025]
 * [Script for detecting targetting something]
 */

public partial class MissileDetection : Node2D
{
	[Export] private MissileEngage _missileEngage;
	[Export] private MissileMovement _missileMovement;

	private List<RayCast2D> _raycasts;
    private bool _canDetect = false;
    private bool _lockedOn = false;

    public override void _Ready()
    {
        _raycasts = new List<RayCast2D>();
        Godot.Collections.Array<Node> nodes = GetChildren();

        foreach (Node node in nodes)
        {
            if (node is RayCast2D)
            {
                _raycasts.Add(node as RayCast2D);
            }
        }
    }

    /// <summary>
    /// every frame, check if raycast are hitting
    /// </summary>
    /// <param name="delta"></param>
    public override void _Process(double delta)
    {
        if (_canDetect && !_lockedOn)
        {
            foreach (RayCast2D ray in _raycasts)
            {
                if (ray.IsColliding())
                {
                    _lockedOn = true;
                    _missileEngage.LockedOnLight();
                    _missileMovement.SetTarget(ray.GetCollider() as Node2D);

                    foreach (RayCast2D ray1 in _raycasts)
                    {
                        ray1.Enabled = false;
                    }

                    break;
                }
            }
        }
    }

    /// <summary>
    /// when missile is armed, can detect
    /// </summary>
    private void OnArmed()
    {
        _canDetect = true;
    }
}
