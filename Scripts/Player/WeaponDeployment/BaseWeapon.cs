using Godot;
using System;

/*
 * Author: [Lam, Justin]
 * Last Updated: [02/21/2025]
 * [base class for activating all player weapons]
 */

public partial class BaseWeapon : Node
{
    [Export] protected PlayerResources _playerResources;
	[Export] protected int _scrapCost;
	[Export] protected float _fuelCost;

	[Export] protected float _cooldown;
	[Export] protected Timer _cooldownTimer;
    protected bool _canUseWeapon = true;

    /// <summary>
    /// sets timer
    /// </summary>
    public override void _Ready()
    {
        _cooldownTimer.WaitTime = _cooldown;
        _cooldownTimer.OneShot = true;
    }

    /// <summary>
    /// when the player inputs the weapon, it calls to use weapon
    /// NOTE:
    /// the input of the weapon needs to be over ridden
    /// needs to use the bas feature when over ridden
    /// 
    /// </summary>
    public virtual void WeaponCall()
    {
        //if input (has to be overrided)
        //if can use weapon
        if (_canUseWeapon && _playerResources.CheckWeaponCost(_scrapCost, _fuelCost))
        {
            _canUseWeapon = false;
            _cooldownTimer.Start();
            Weapon();
        }
        //overrides to use weapon
    }

    /// <summary>
    /// the weapon effect
    /// </summary>
    protected virtual void Weapon() 
    {
        _playerResources.UseScrap(_scrapCost);
        _playerResources.UseFuel(_fuelCost);
    }

    /// <summary>
    /// 
    /// </summary>
    private void OnTimerTimeout()
    {
        _canUseWeapon = true;
    }
}
