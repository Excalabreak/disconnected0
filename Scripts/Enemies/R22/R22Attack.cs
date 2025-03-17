using Godot;
using System;
using System.Collections.Generic;

/*
 * Author: [Lam, Justin]
 * Last Updated: [03/15/2025]
 * [attack script for R22]
 */

public partial class R22Attack : BaseEnemyAttack
{
    [Export] private Node2D _r22;
    [Export] private MultiroleMovement _movement;

    //laser raycasts
    private List<RayCast2D> _raycasts;
    [Export] private float _detectLength = -800f;
    [Export] private int[] _mask;
    [Export] private float _degreesBetween = 3f;
    [Export] private int _amountOfRaycasts = 7;

    //shooting guns
    [Export] private PackedScene _bullet;
    [Export] private float _gunsRange = 1000f;
    [Export] private Timer _fireRateTimer;
    [Export] private float _fireRate = .5f;
    [Export] private int _burstAmount = 10;
    [Export] private Timer _reloadTimer;
    [Export] private float _reloadTime = 5f;
    [Export] private float _range = 1f;

    private bool _canGun = true;
    private int _currentBullets;

    //missile
    [Export] private PackedScene _missile;
    [Export] private Timer _missileTimer;
    [Export] private float _missileCooldown = 3f;
    private bool _canMissile = true;

    /// <summary>
    /// sets up on ready
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

        _missileTimer.WaitTime = _missileCooldown;
        _missileTimer.OneShot = true;
        _missileTimer.Autostart = false;
    }

    public override void Attack()
    {
        foreach (RayCast2D ray in _raycasts)
        {
            if (ray.IsColliding())
            {
                //in close distance with bullets
                if (_r22.GlobalPosition.DistanceTo(GameManager.instance.currentPlayer.GlobalPosition) <= _gunsRange &&
                    _canGun)
                {
                    //shoot "bullet" (just reusing laser)
                    Laser laser = _bullet.Instantiate<Laser>() as Laser;
                    laser.GlobalPosition = _r22.GlobalPosition;

                    laser.GlobalRotation = _r22.GlobalRotation + (float)GD.RandRange(-_range, _range);
                    laser.Position -= laser.Transform.Y * 25;

                    GetTree().Root.AddChild(laser);

                    _canGun = false;

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
                else if (_r22.GlobalPosition.DistanceTo(GameManager.instance.currentPlayer.GlobalPosition) > _gunsRange &&
                    _canMissile)
                {
                    Missile missile = _missile.Instantiate<Missile>() as Missile;
                    missile.GlobalPosition = _r22.GlobalPosition;
                    missile.GlobalRotation = _r22.GlobalRotation;
                    missile.missileMovement.SetInitialVelocity(_movement.velocity);
                    GetTree().Root.AddChild(missile);

                    _canMissile = false;
                    _missileTimer.Start();
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

    private void OnBulletTimeout()
    {
        _canGun = true;
    }

    private void OnMissileTimeout()
    {
        _canMissile = true;
    }
}
