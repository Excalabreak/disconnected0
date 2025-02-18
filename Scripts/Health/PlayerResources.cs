using Godot;
using System;

/*
 * Author: [Lam, Justin]
 * Last Updated: [02/15/2025]
 * [script for player's Resources (health)]
 */

public partial class PlayerResources : BaseHealth
{
    [Export] private int _maxScrap = 10;
    [Export] private float _maxFuel = 10f;

    private int _currentScrap;
	private float _currentFuel;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_currentScrap = _maxScrap;
		_currentFuel = _maxFuel;
		UpdateUI();

    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
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

        if (_currentScrap < 0 || _currentFuel <= 0)
        {
			GameOver();
        }
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
	/// Only when the player trys to use scrap
	/// checks if the amount is available.
	/// if true: takes out the scrap and returns true
	/// if false: then does nothing and return false
	/// </summary>
	/// <param name="scrap">amount of scrap use</param>
	/// <returns>did the player use the scrap</returns>
	public bool UseScrap(int scrap)
	{
		if (scrap < _currentScrap)
		{
			_currentScrap -= scrap;
            UpdateUI();
            return true;
		}
		return false;
	}

    /// <summary>
    /// Only when the player trys to use fuel
    /// checks if the amount is available.
    /// if true: takes out the fuel and returns true
    /// if false: then does nothing and return false
    /// </summary>
    /// <param name="fuel">amount of fuel use</param>
    /// <returns>did the player use the fuel</returns>
    public bool UseFuel(float fuel)
    {
        if (fuel < _currentFuel)
        {
            _currentFuel -= fuel;
            UpdateUI();
            return true;
        }
        return false;
    }

	/// <summary>
	/// temp game over function
	/// </summary>
    private void GameOver()
	{
		GD.Print("Game Over");
	}

	/// <summary>
	/// updates UI
	/// </summary>
	private void UpdateUI()
	{
		UIManager._instance.SetResourceLabels(_currentScrap, _currentFuel);
	}
}
