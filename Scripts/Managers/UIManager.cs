using Godot;
using System;

/*
 * Author: [Lam, Justin]
 * Last Updated: [02/15/2025]
 * [Singleton to manage UI]
 */

public partial class UIManager : Node
{
	public static UIManager _instance;

    [Export] private Label _scrapLabel;
    [Export] private Label _fuelLabel;

    /// <summary>
    /// sets up singleton
    /// </summary>
    public override void _Ready()
    {
        if(_instance != null)
        {
            QueueFree();
        }

        _instance = this;
    }

    public void SetResourceLabels(int scrap, float fuel)
    {
        _scrapLabel.Text = "Scrap: " + scrap;
        _fuelLabel.Text = "Fuel: " + fuel;
    }
}
