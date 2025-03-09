using Godot;
using System;
using System.Net;

/*
 * Author: [Lam, Justin]
 * Last Updated: [03/09/2025]
 * [switches bool in _bomberEnemyScript
 * (could technically use a state machine, 
 * but it's 2 states)]
 */

public partial class BomberRanges : Node2D
{
    [Export] private Node2D _bomber;
    [Export] private BomberEnemyScript _bomberEnemyScript;

    //distance to switch run to deploy
    [Export] private float _disRunSwitch = 500f;

    //distance to switch deploy to run
    [Export] private float _disDeploySwitch = 500f;

    /// <summary>
    /// checks the ranges of the states
    /// </summary>
    public void CheckState()
    {
        if (_bomberEnemyScript.InRun)
        {
            if (DistanceToPlayer() > _disRunSwitch)
            {
                _bomberEnemyScript.InRun = false;
            }
        }
        else
        {
            if (DistanceToPlayer() < _disDeploySwitch)
            {
                _bomberEnemyScript.InRun = true;
            }
        }
    }

    /// <summary>
    /// gets the distance from the bomber to player
    /// </summary>
    /// <returns>distance</returns>
    private float DistanceToPlayer()
    {
        return _bomber.GlobalPosition.DistanceTo(GameManager.instance.currentPlayer.GlobalPosition);
    }
}
