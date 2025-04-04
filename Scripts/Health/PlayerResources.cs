using Godot;
using System;

/*
 * Author: [Lam, Justin]
 * Last Updated: [02/15/2025]
 * [script for player's Resources (health)]
 */

public partial class PlayerResources : BaseHealth
{
	[Export] private PlayerMovement _playerMovement;

    [Export] private int _maxScrap = 10;
    [Export] private float _maxFuel = 10f;
    [Export] private Timer _fuelTimer;
    [Export] private float _baseFuelConsumption = .1f;
    [Export] private float _movingFuelConsumption = .1f;
	[Export] private float _baseFuelTime = 1f;

    private int _currentScrap;
	private float _currentFuel;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_currentScrap = _maxScrap;
		_currentFuel = _maxFuel;
		UpdateUI();

        _fuelTimer.WaitTime = _baseFuelTime;
        _fuelTimer.OneShot = true;
        _fuelTimer.Autostart = false;

		_fuelTimer.Start();
    }

	/// <summary>
	/// when the player recieves damage,
	/// take damage from both fuel and scrap
	/// </summary>
	/// <param name="damage">how much damage is taken</param>
    public override void Damage(int damage)
    {
        _currentScrap -= damage;
		_currentFuel -= (float)damage;
        UpdateUI();

		CheckForGameOver();
    }

	/// <summary>
	/// when the player gains scrap
	/// </summary>
	/// <param name="scrap">amount of scrap recieved</param>
	public void GainScrap(int scrap)
	{
		if (_currentScrap + scrap <= _maxScrap)
		{
            _currentScrap += scrap;
        }
		else
		{
			_currentScrap = _maxScrap;
        }
        UpdateUI();
    }

    /// <summary>
    /// when the player gains fuel
    /// </summary>
    /// <param name="fuel">amount of fuel recieved</param>
    public void GainFuel(float fuel)
    {
		if (_currentFuel + fuel <= _maxFuel)
		{
            _currentFuel += fuel;
		}
		else
		{
			_currentFuel = _maxFuel;
		}
        UpdateUI();
    }

	/// <summary>
	/// uses scrap if possible
	/// </summary>
	/// <param name="scrap">amount of scrap use</param>
	public void UseScrap(int scrap)
	{
		if (scrap < _currentScrap)
		{
			_currentScrap -= scrap;
            UpdateUI();
		}
	}

    /// <summary>
	/// usses fuel if possible
	/// </summary>
	/// <param name="fuel">amount of fuel used</param>
    public void UseFuel(float fuel)
    {
        if (fuel < _currentFuel)
        {
            _currentFuel -= fuel;
            UpdateUI();
        }
    }

	/// <summary>
	/// checks if a weapon can be used based off of resources
	/// </summary>
	/// <param name="scrap">amount of scrap</param>
	/// <param name="fuel">amount of fuel</param>
	/// <returns>can the weapon be used</returns>
	public bool CheckWeaponCost(int scrap, float fuel)
	{
		return (scrap < _currentScrap && fuel < _currentFuel);
	}

	/// <summary>
	/// temp game over function
	/// </summary>
    private void GameOver()
	{
		GameManager.instance.GameOver();
	}

	/// <summary>
	/// fuel slowly goes down causing players to need to collect fuel
	/// </summary>
	private void OnTimerTimeout()
	{
		_currentFuel -= _baseFuelConsumption;
		if (_playerMovement.thrust > 0)
		{
            _currentFuel -= _movingFuelConsumption;
        }

		UpdateUI();
		CheckForGameOver();
        _fuelTimer.Start();
    }

	private void CheckForGameOver()
	{
        if (_currentScrap < 0 || _currentFuel <= 0)
        {
            GameOver();
        }
    }

    /// <summary>
    /// updates UI
    /// </summary>
    private void UpdateUI()
	{
		UIManager.instance.SetResourceLabels(_currentScrap, _currentFuel);
	}
}
