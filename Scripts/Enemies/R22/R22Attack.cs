using Godot;
using System;
using System.Collections.Generic;

/*
 * Author: [Lam, Justin]
 * Last Updated: [03/14/2025]
 * [attack script for R22]
 */

public partial class R22Attack : BaseEnemyAttack
{
    [Export] private Node2D _r22;

    //laser raycasts
    private List<RayCast2D> _raycasts;
    [Export] private float _detectLength = -800f;
    [Export] private int[] _mask;
    [Export] private float _degreesBetween = 3f;
    [Export] private int _amountOfRaycasts = 7;

    //shooting laser
    [Export] private PackedScene _bullet;
    [Export] private float _gunsRange = 1000f;
    [Export] private Timer _fireRateTimer;
    [Export] private float _fireRate = .5f;
    [Export] private int _burstAmount = 10;
    [Export] private Timer _reloadTimer;
    [Export] private float _reloadTime = 5f;
    [Export] private float _range = 1f;

    private bool _canFire = true;
    private int _currentBullets;

    /// <summary>
    /// sets up detection
    /// </summary>
    public override void _Ready()
    {
        _currentBullets = _burstAmount;
        _raycasts = new List<RayCast2D>();

        CreateRaycast(0);
        for (int i = 1; i <= _amountOfRaycasts; i++)
        {
            CreateRaycast(_degreesBetween * i);
            CreateRaycast(_degreesBetween * (-i));
        }

        _fireRateTimer.WaitTime = _fireRate;
        _fireRateTimer.OneShot = true;
        _fireRateTimer.Autostart = false;

        _reloadTimer.WaitTime = _reloadTime;
        _reloadTimer.OneShot = true;
        _reloadTimer.Autostart = false;
    }

    public override void Attack()
    {
        foreach (RayCast2D ray in _raycasts)
        {
            if (ray.IsColliding())
            {
                //in close distance with bullets
                if (_r22.GlobalPosition.DistanceTo(GameManager.instance.currentPlayer.GlobalPosition) <= _gunsRange &&
                    _canFire)
                {
                    //shoot laser
                    Laser laser = _bullet.Instantiate<Laser>() as Laser;
                    laser.GlobalPosition = _r22.GlobalPosition;

                    laser.GlobalRotation = _r22.GlobalRotation + (float)GD.RandRange(-_range, _range);
                    GetTree().Root.AddChild(laser);
                    laser.Position -= laser.Transform.Y * 25;

                    _canFire = false;

                    _currentBullets--;
                    if (_currentBullets <= 0)
                    {
                        _currentBullets = _burstAmount;
                        _reloadTimer.Start();
                    }
                    else
                    {
                        _fireRateTimer.Start();
                    }
                    break;
                }
                //add missile
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

    private void OnBulletTimeout()
    {
        _canFire = true;
    }
}
