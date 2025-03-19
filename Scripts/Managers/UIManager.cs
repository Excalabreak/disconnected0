using Godot;
using System;

/*
 * Author: [Lam, Justin]
 * Last Updated: [03/19/2025]
 * [Singleton to manage UI]
 */

public partial class UIManager : Node
{
	private static UIManager _instance;

    [Export] private Label _scrapLabel;
    [Export] private Label _fuelLabel;

    [Export] private Sprite2D _planetImage;
    [Export] private Texture2D[] _backgroundPlanets;

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
        _fuelLabel.Text = "Fuel: " + fuel.ToString("F1");
    }

    public void SetBackgroundPlanet(Planets planet)
    {
        _planetImage.Texture = _backgroundPlanets[(int)planet];
    }

    public static UIManager instance
    {
        get { return _instance; }
    }
}
