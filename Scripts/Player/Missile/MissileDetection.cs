using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

/*
 * Author: [Lam, Justin]
 * Last Updated: [03/15/2025]
 * [Script for detecting targetting something]
 */

public partial class MissileDetection : Node2D
{
	[Export] private MissileEngage _missileEngage;
	[Export] private MissileMovement _missileMovement;

	private List<RayCast2D> _raycasts;
    [Export] private float _detectLength = -200f;
    [Export] private int[] _mask;
    [Export] private float _degreesBetween = 5f;
    [Export] private int _amountOfRaycasts = 9;

    private bool _canDetect = false;
    private bool _lockedOn = false;

    public override void _Ready()
    {
        _raycasts = new List<RayCast2D>();

        CreateRaycast(0);
        for (int i = 1; i <= _amountOfRaycasts; i++)
        {
            CreateRaycast(_degreesBetween * i);
            CreateRaycast(_degreesBetween * (-i));
        }
    }

    public void CheckDetection()
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
    /// creates raycast for detection range
    /// and adds them to list
    /// </summary>
    private void CreateRaycast(float degrees)
    {
        Vector2 raycastLength = new Vector2(0, _detectLength);

        RayCast2D ray = new RayCast2D();
        ray.Enabled = true;
        ray.ExcludeParent = true;
        ray.TargetPosition = raycastLength;
        ray.SetCollisionMaskValue(1, false);
        foreach (int i in _mask)
        {
            ray.SetCollisionMaskValue(i, true);
        }
        ray.CollideWithAreas = true;
        ray.CollideWithBodies = false;

        ray.RotationDegrees = degrees;

        AddChild(ray);
        _raycasts.Add(ray);
    }

    /// <summary>
    /// when missile is armed, can detect
    /// </summary>
    private void OnArmed()
    {
        _canDetect = true;
    }
}
