using Godot;
using System;
using System.Collections.Generic;

/*
 * Author: [Lam, Justin]
 * Last Updated: [03/12/2025]
 * [Attack script for multirole]
 */

public partial class MultiroleAttack : BaseEnemyAttack
{
    [Export] private Node2D _MultirolePos;

    //laser raycasts
    private List<RayCast2D> _raycasts;
    [Export] private float _detectLength = -500f;
    [Export] private int[] _mask;
    [Export] private float _degreesBetween = 5f;
    [Export] private int _amountOfRaycasts = 7;

    //shooting laser
    [Export] private PackedScene _laser;
    [Export] private Timer _laserTimer;
    [Export] private float _laserCooldown = 2f;
    [Export] private float _range = 20f;
    private bool _laserInCooldown = false;

    public override void _Ready()
    {
        _raycasts = new List<RayCast2D>();

        CreateRaycast(0);
        for (int i = 1; i <= _amountOfRaycasts; i++)
        {
            CreateRaycast(_degreesBetween * i);
            CreateRaycast(_degreesBetween * (-i));
        }

        _laserTimer.WaitTime = _laserCooldown;
        _laserTimer.OneShot = true;
        _laserTimer.Autostart = false;
    }

    /// <summary>
    /// checks for the laser shots
    /// </summary>
    public override void Attack()
    {
        if (!_laserInCooldown)
        {
            foreach (RayCast2D ray in _raycasts)
            {
                if (ray.IsColliding())
                {
                    //shoot laser
                    Laser laser = _laser.Instantiate<Laser>() as Laser;
                    laser.GlobalPosition = _MultirolePos.GlobalPosition;

                    Vector2 dir = (GameManager.instance.currentPlayer.GlobalPosition +
                        new Vector2((float)GD.RandRange(-_range, _range), (float)GD.RandRange(-_range, _range)))
                        - laser.GlobalPosition;

                    laser.GlobalRotation = (-laser.Transform.Y).AngleTo(dir);
                    GD.Print(laser.GlobalRotation);
                    GetTree().Root.AddChild(laser);

                    _laserInCooldown = true;
                    _laserTimer.Start();
                    break;
                }
            }
        }
    }

    /// <summary>
    /// sets laser cooldown to be false after
    /// </summary>
    private void OnLaserTimerTimeout()
    {
        _laserInCooldown = false;
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
}
