using Godot;
using System;

/*
 * Author: [Lam, Justin]
 * Last Updated: [03/26/2025]
 * [Singleton to manage UI]
 */

public partial class UIManager : Node
{
	private static UIManager _instance;

    [Export] private PanelContainer _resourceContainer;
    [Export] private Label _scrapLabel;
    [Export] private Label _fuelLabel;

    [Export] private Sprite2D _planetImage;
    [Export] private Texture2D[] _backgroundPlanets;

    [Export] private PanelContainer _menuContainer;
    [Export] private Label _cotrolsLabel;
    [Export] private Label _titleLabel;


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

    /// <summary>
    /// toggle to show the menu
    /// </summary>
    /// <param name="showMenu"></param>
    private void ToggleMenu(bool showMenu)
    {
        _menuContainer.Visible = showMenu;
    }

    private void ToggleResource(bool showResources)
    {
        _resourceContainer.Visible = showResources;
    }

    /// <summary>
    /// for when the play button is pressed
    /// </summary>
    private void OnPlayPressed()
    {


        ToggleMenu(false);
    }

    /// <summary>
    /// quits out of game when pressed
    /// </summary>
    private void OnQuitPressed()
    {
        GetTree().Quit();
    }

    /// <summary>
    /// toggles between showing and hiding controls
    /// </summary>
    /// <param name="toggled_on">state of toggle</param>
    private void ToggleControls(bool toggled_on)
    {
        _cotrolsLabel.Visible = toggled_on;
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
